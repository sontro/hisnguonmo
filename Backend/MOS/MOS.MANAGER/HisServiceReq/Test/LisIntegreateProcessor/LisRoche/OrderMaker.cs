using AutoMapper;
using MOS.EFMODEL.DataModels;
using MOS.LIS.RocheV2;
using MOS.LIS.RocheV3;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisRoche
{
    class OrderMaker
    {
        public static List<RocheAstmOrderData> MakeUpdateOrderData(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> inserts, List<HIS_SERE_SERV> deletes)
        {
            List<RocheAstmOrderData> result = new List<RocheAstmOrderData>();
            RocheAstmOrderData cancel = MakeOrderData(serviceReq, deletes, OrderType.CANCEL_TEST);
            RocheAstmOrderData add = MakeOrderData(serviceReq, inserts, OrderType.ADD_TEST);
            if (cancel != null)
            {
                result.Add(cancel);
            }
            if (add != null)
            {
                result.Add(add);
            }
            return result;
        }

        public static RocheHl7OrderData MakeUpdateHl7OrderData(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> inserts, List<HIS_SERE_SERV> deletes)
        {
            List<RocheHl7OrderData> result = new List<RocheHl7OrderData>();
            if (inserts != null && inserts.Count > 0
                && deletes != null && deletes.Count > 0)
            {
                throw new Exception("HL7 ko ho tro them va xoa trong cung 1 ban tin order");
            }

            if (deletes != null && deletes.Count > 0)
            {
                return MakeHl7OrderData(serviceReq, deletes, Hl7OrderType.CANCEL_TEST);
            }

            if (inserts != null && inserts.Count > 0)
            {
                return MakeHl7OrderData(serviceReq, inserts, Hl7OrderType.ADD_TEST);
            }
            return null;
        }

        public static RocheAstmOrderData MakeOrderData(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> sereServs, OrderType type)
        {
            if (serviceReq != null && sereServs != null && sereServs.Count > 0)
            {
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.REQUEST_DEPARTMENT_ID).FirstOrDefault();
                HIS_BRANCH branch = HisBranchCFG.DATA.Where(o => o.ID == department.BRANCH_ID).FirstOrDefault();
                HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == sereServs[0].PATIENT_TYPE_ID).FirstOrDefault();

                RocheAstmOrderData order = new RocheAstmOrderData();

                if (LisRocheCFG.MESSAGE_FORMAT_IS_USING_PATIENT_TYPE_INSTEAD_OF_DEPARTMENT_CODE)
                {
                    order.DepartmentCode = patientType != null ? patientType.PATIENT_TYPE_CODE : "";
                }
                else
                {
                    order.DepartmentCode = department != null ? department.DEPARTMENT_CODE : "";
                }

                if (LisRocheCFG.MESSAGE_FORMAT_ORDER_INFO_IS_HAVING_DOCTOR_NAME)
                {
                    order.DoctorName = serviceReq.REQUEST_USERNAME;
                }
                order.BranchCode = branch.BRANCH_CODE;
                order.OrderCode = OrderMaker.ReduceOrderCode(serviceReq.BARCODE);
                order.OrderDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME).Value;
                order.Type = type;
                order.TestIndexCodes = OrderMaker.GetTestIndexCode(sereServs);
                return order;
            }
            return null;
        }

        public static RocheHl7OrderData MakeHl7OrderData(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> sereServs, Hl7OrderType type)
        {
            if (serviceReq != null && sereServs != null && sereServs.Count > 0)
            {
                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.REQUEST_DEPARTMENT_ID).FirstOrDefault();
                HIS_BRANCH branch = HisBranchCFG.DATA.Where(o => o.ID == department.BRANCH_ID).FirstOrDefault();
                HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == sereServs[0].PATIENT_TYPE_ID).FirstOrDefault();

                RocheHl7OrderData order = new RocheHl7OrderData();

                if (LisRocheCFG.MESSAGE_FORMAT_IS_USING_PATIENT_TYPE_INSTEAD_OF_DEPARTMENT_CODE)
                {
                    order.DepartmentCode = patientType != null ? patientType.PATIENT_TYPE_CODE : "";
                    order.DepartmentName = patientType != null ? patientType.PATIENT_TYPE_NAME : "";
                }
                else
                {
                    order.DepartmentCode = department != null ? department.DEPARTMENT_CODE : "";
                    order.DepartmentName = department != null ? department.DEPARTMENT_NAME : "";
                }

                if (LisRocheCFG.MESSAGE_FORMAT_ORDER_INFO_IS_HAVING_DOCTOR_NAME)
                {
                    order.DoctorName = serviceReq.REQUEST_USERNAME;
                }
                order.BranchId = branch.ID.ToString();
                order.BranchName = branch.BRANCH_NAME;

                order.OrderCode = OrderMaker.ReduceOrderCode(serviceReq.BARCODE);
                order.OrderDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME).Value;
                order.Type = type;
                order.DoctorName = serviceReq.REQUEST_USERNAME;
                order.IcdCode = serviceReq.ICD_CODE;
                order.IcdName = serviceReq.ICD_NAME;
                order.IsBhyt = serviceReq.TDL_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                order.IsEmergency = serviceReq.IS_EMERGENCY == Constant.IS_TRUE;
                order.NumOrder = serviceReq.CALL_SAMPLE_ORDER;
                order.RequestLoginName = serviceReq.REQUEST_LOGINNAME;
                order.RequestUserName = serviceReq.REQUEST_USERNAME;
                order.TestIndexs = OrderMaker.GetTestIndex(sereServs);
                return order;
            }
            return null;
        }

        private static string ReduceOrderCode(string orderCode)
        {
            if (!string.IsNullOrWhiteSpace(orderCode) && orderCode.Length >= LisRocheCFG.MESSAGE_FORMAT_ORDER_INFO_ORDER_CODE_LENGTH)
            {
                return orderCode.Substring(orderCode.Length - LisRocheCFG.MESSAGE_FORMAT_ORDER_INFO_ORDER_CODE_LENGTH);
            }
            return orderCode;
        }

        private static List<string> GetTestIndexCode(List<HIS_SERE_SERV> sereServs)
        {
            return HisTestIndexCFG.DATA_VIEW
                .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                    && sereServs.Exists(t => t.SERVICE_ID == o.TEST_SERVICE_TYPE_ID))
                .Select(o => o.TEST_INDEX_CODE).ToList();
        }

        private static List<RocheHl7TestIndexData> GetTestIndex(List<HIS_SERE_SERV> sereServs)
        {
            return HisTestIndexCFG.DATA_VIEW
                .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE
                    && sereServs.Exists(t => t.SERVICE_ID == o.TEST_SERVICE_TYPE_ID))
                .Select(o => new RocheHl7TestIndexData
                {
                    TestIndexCode = o.TEST_INDEX_CODE,
                    TestIndexName = o.TEST_INDEX_NAME
                }).ToList();
        }
    }
}