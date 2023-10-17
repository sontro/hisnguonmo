using Inventec.Common.ObjectChecker;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ
{
    class HisSereServUtil
    {
        public static void GetChangeRecord(List<HIS_SERE_SERV> beforeChanges, List<HIS_SERE_SERV> afterChanges, ref List<HIS_SERE_SERV> changeList, ref List<HIS_SERE_SERV> oldOfchangeList)
        {
            if (beforeChanges != null && beforeChanges.Count > 0 && afterChanges != null && afterChanges.Count > 0)
            {
                changeList = new List<HIS_SERE_SERV>();
                oldOfchangeList = new List<HIS_SERE_SERV>();
                foreach (HIS_SERE_SERV before in beforeChanges)
                {
                    HIS_SERE_SERV after = afterChanges.Where(o => o.ID == before.ID).FirstOrDefault();
                    if (after != null && ValueChecker.IsPrimitiveDiff(before, after))
                    {
                        changeList.Add(after);
                        oldOfchangeList.Add(before);
                    }
                }
            }
        }

        /// <summary>
        /// Lay ra danh sach thay doi ma se bi chan trong truong hop co thong tin hoa don
        /// (Cac ban ghi neu thay doi vao cac du lieu lien quan den gia hoac thong tin cua dich vu)
        /// </summary>
        /// <param name="changes"></param>
        /// <param name="olds"></param>
        /// <returns></returns>
        public static List<HIS_SERE_SERV> GetUnallowUpdateWhenHavingInvoice(List<HIS_SERE_SERV> changes, List<HIS_SERE_SERV> olds)
        {
            List<HIS_SERE_SERV> unallowUpdates = new List<HIS_SERE_SERV>();
            foreach (HIS_SERE_SERV s in changes)
            {
                HIS_SERE_SERV old = olds.Where(o => o.ID == s.ID).FirstOrDefault();
                if (old != null
                    && (old.ADD_PRICE != s.ADD_PRICE
                        || old.AMOUNT != s.AMOUNT
                        || old.BLOOD_ID != s.BLOOD_ID
                        || old.DISCOUNT != s.DISCOUNT
                        || old.EXP_MEST_MATERIAL_ID != s.EXP_MEST_MATERIAL_ID
                        || old.EXP_MEST_MEDICINE_ID != s.EXP_MEST_MEDICINE_ID
                        || old.HEIN_APPROVAL_ID != s.HEIN_APPROVAL_ID
                        || old.HEIN_LIMIT_PRICE != s.HEIN_LIMIT_PRICE
                        || old.HEIN_LIMIT_RATIO != s.HEIN_LIMIT_RATIO
                        || old.HEIN_NORMAL_PRICE != s.HEIN_NORMAL_PRICE
                        || old.HEIN_PRICE != s.HEIN_PRICE
                        || old.HEIN_RATIO != s.HEIN_RATIO
                        || old.IS_EXPEND != s.IS_EXPEND
                        || old.IS_NO_EXECUTE != s.IS_NO_EXECUTE
                        || old.IS_NO_HEIN_DIFFERENCE != s.IS_NO_HEIN_DIFFERENCE
                        || old.IS_NO_PAY != s.IS_NO_PAY
                        || old.IS_OUT_PARENT_FEE != s.IS_OUT_PARENT_FEE
                    //de tranh truong hop cac version cu, chua co truong PRIMARY_PRICE
                        || (old.LIMIT_PRICE.HasValue && old.LIMIT_PRICE != s.LIMIT_PRICE)
                        || old.MATERIAL_ID != s.MATERIAL_ID
                        || old.MEDICINE_ID != s.MEDICINE_ID
                        || old.ORIGINAL_PRICE != s.ORIGINAL_PRICE
                        || old.OVERTIME_PRICE != s.OVERTIME_PRICE
                        || old.PACKAGE_ID != s.PACKAGE_ID
                        || old.PARENT_ID != s.PARENT_ID
                        || old.PATIENT_TYPE_ID != s.PATIENT_TYPE_ID
                        || old.PRICE != s.PRICE
                        || old.PRIMARY_PATIENT_TYPE_ID != s.PRIMARY_PATIENT_TYPE_ID
                    //de tranh truong hop cac version cu, chua co truong PRIMARY_PRICE
                        || (old.PRIMARY_PRICE.HasValue && old.PRIMARY_PRICE != s.PRIMARY_PRICE)
                        || old.SERVICE_ID != s.SERVICE_ID
                        || old.SERVICE_REQ_ID != s.SERVICE_REQ_ID
                        || old.SHARE_COUNT != s.SHARE_COUNT
                        || old.TDL_BILL_OPTION != s.TDL_BILL_OPTION
                        || old.TDL_EXECUTE_BRANCH_ID != s.TDL_EXECUTE_BRANCH_ID
                        || old.TDL_EXECUTE_DEPARTMENT_ID != s.TDL_EXECUTE_DEPARTMENT_ID
                        || old.TDL_EXECUTE_ROOM_ID != s.TDL_EXECUTE_ROOM_ID
                        || old.TDL_HEIN_SERVICE_TYPE_ID != s.TDL_HEIN_SERVICE_TYPE_ID
                        || old.TDL_IS_MAIN_EXAM != s.TDL_IS_MAIN_EXAM
                        || old.TDL_PATIENT_ID != s.TDL_PATIENT_ID
                        || old.TDL_SERVICE_CODE != s.TDL_SERVICE_CODE
                        || old.TDL_SERVICE_NAME != s.TDL_SERVICE_NAME
                        || old.TDL_SERVICE_REQ_CODE != s.TDL_SERVICE_REQ_CODE
                        || old.TDL_SERVICE_REQ_TYPE_ID != s.TDL_SERVICE_REQ_TYPE_ID
                        || old.TDL_SERVICE_UNIT_ID != s.TDL_SERVICE_UNIT_ID
                        || old.TDL_TREATMENT_CODE != s.TDL_TREATMENT_CODE
                        || old.TDL_TREATMENT_ID != s.TDL_TREATMENT_ID
                        || old.VAT_RATIO != s.VAT_RATIO
                        || old.PATIENT_PRICE_BHYT != s.PATIENT_PRICE_BHYT
                        || old.OTHER_SOURCE_PRICE != s.OTHER_SOURCE_PRICE
                        )
                )
                {
                    unallowUpdates.Add(old);
                }
            }
            return unallowUpdates;
        }

        //Luu du thua du lieu
        public static void SetTdl(HIS_SERE_SERV sereServ, HIS_SERVICE_REQ serviceReq)
        {
            if (sereServ != null && serviceReq != null)
            {
                HIS_DEPARTMENT executeDepartment = HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.EXECUTE_DEPARTMENT_ID).FirstOrDefault();
                sereServ.TDL_PATIENT_ID = serviceReq.TDL_PATIENT_ID;
                sereServ.TDL_EXECUTE_DEPARTMENT_ID = serviceReq.EXECUTE_DEPARTMENT_ID;
                sereServ.TDL_EXECUTE_ROOM_ID = serviceReq.EXECUTE_ROOM_ID;
                sereServ.TDL_REQUEST_DEPARTMENT_ID = serviceReq.REQUEST_DEPARTMENT_ID;
                sereServ.TDL_REQUEST_LOGINNAME = serviceReq.REQUEST_LOGINNAME;
                sereServ.TDL_REQUEST_USER_TITLE = serviceReq.REQUEST_USER_TITLE;
                sereServ.TDL_REQUEST_ROOM_ID = serviceReq.REQUEST_ROOM_ID;
                sereServ.TDL_REQUEST_USERNAME = serviceReq.REQUEST_USERNAME;
                sereServ.TDL_SERVICE_REQ_CODE = serviceReq.SERVICE_REQ_CODE;
                sereServ.TDL_TREATMENT_CODE = serviceReq.TDL_TREATMENT_CODE;
                sereServ.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                sereServ.TDL_INTRUCTION_TIME = serviceReq.INTRUCTION_TIME;
                sereServ.TDL_INTRUCTION_DATE = sereServ.TDL_INTRUCTION_TIME - sereServ.TDL_INTRUCTION_TIME % 1000000;
                sereServ.TDL_EXECUTE_BRANCH_ID = executeDepartment.BRANCH_ID;
                sereServ.TDL_SERVICE_REQ_TYPE_ID = serviceReq.SERVICE_REQ_TYPE_ID;
                sereServ.TDL_IS_MAIN_EXAM = serviceReq.IS_MAIN_EXAM;
                sereServ.TDL_RATION_TIME_ID = serviceReq.RATION_TIME_ID;

                V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW != null ? HisServiceCFG.DATA_VIEW.Where(o => o.ID == sereServ.SERVICE_ID).FirstOrDefault() : null;
                if (service == null) //neu null ==> co the do chua duoc cap nhat vao cache, ==> reload de lay thong tin moi nhat
                {
                    HisServiceCFG.Reload();
                }
                service = HisServiceCFG.DATA_VIEW != null ? HisServiceCFG.DATA_VIEW.Where(o => o.ID == sereServ.SERVICE_ID).FirstOrDefault() : null;
                if (service == null)
                {
                    throw new Exception("service_id khong hop le");
                }

                sereServ.TDL_HEIN_ORDER = service.HEIN_ORDER;
                sereServ.TDL_HEIN_SERVICE_BHYT_CODE = service.HEIN_SERVICE_BHYT_CODE;
                sereServ.TDL_HEIN_SERVICE_BHYT_NAME = service.HEIN_SERVICE_BHYT_NAME;
                sereServ.TDL_HEIN_SERVICE_TYPE_ID = service.HEIN_SERVICE_TYPE_ID;
                sereServ.TDL_SERVICE_CODE = service.SERVICE_CODE;
                sereServ.TDL_SPECIALITY_CODE = service.SPECIALITY_CODE;
                sereServ.TDL_SERVICE_NAME = service.SERVICE_NAME;
                sereServ.TDL_SERVICE_TYPE_ID = service.SERVICE_TYPE_ID;
                sereServ.TDL_SERVICE_UNIT_ID = service.SERVICE_UNIT_ID;
                sereServ.TDL_HST_BHYT_CODE = service.HEIN_SERVICE_TYPE_BHYT_CODE;
                sereServ.TDL_PACS_TYPE_CODE = service.PACS_TYPE_CODE;
                sereServ.TDL_BILL_OPTION = service.BILL_OPTION;
                sereServ.TDL_IS_SPECIFIC_HEIN_PRICE = service.IS_SPECIFIC_HEIN_PRICE;
                sereServ.IS_OTHER_SOURCE_PAID = service.IS_OTHER_SOURCE_PAID;
                sereServ.TDL_SERVICE_TAX_RATE_TYPE = service.TAX_RATE_TYPE;
                sereServ.TDL_SERVICE_DESCRIPTION = service.DESCRIPTION;
            }
        }

        //Luu du thua du lieu
        public static void SetTdl(HIS_SERE_SERV sereServ, HIS_SERVICE_REQ serviceReq, V_HIS_MEDICINE_2 medicine)
        {
            HisSereServUtil.SetTdl(sereServ, medicine);
            HisSereServUtil.SetTdl(sereServ, serviceReq);
        }

        public static void SetTdl(HIS_SERE_SERV sereServ, V_HIS_MEDICINE_2 medicine)
        {
            if (medicine != null)
            {
                sereServ.TDL_ACTIVE_INGR_BHYT_CODE = medicine.ACTIVE_INGR_BHYT_CODE;
                sereServ.TDL_ACTIVE_INGR_BHYT_NAME = medicine.ACTIVE_INGR_BHYT_NAME;
                sereServ.TDL_MEDICINE_BID_NUM_ORDER = medicine.TDL_BID_NUM_ORDER;
                sereServ.TDL_MEDICINE_CONCENTRA = medicine.CONCENTRA;
                sereServ.TDL_MEDICINE_PACKAGE_NUMBER = medicine.PACKAGE_NUMBER;
                sereServ.TDL_MEDICINE_REGISTER_NUMBER = medicine.MEDICINE_REGISTER_NUMBER;
                sereServ.MEDICINE_ID = medicine.ID;
                sereServ.SERVICE_ID = medicine.SERVICE_ID;
            }
        }

        //Luu du thua du lieu
        public static void SetTdl(HIS_SERE_SERV sereServ, HIS_SERVICE_REQ serviceReq, HIS_MATERIAL_TYPE materialType)
        {
            if (materialType != null)
            {
                sereServ.TDL_MATERIAL_GROUP_BHYT = materialType.MATERIAL_GROUP_BHYT;
                sereServ.SERVICE_ID = materialType.SERVICE_ID;
            }
            HisSereServUtil.SetTdl(sereServ, serviceReq);
        }

        public static void CopyInformation(HIS_SERE_SERV old, HIS_SERE_SERV tmp)
        {
            old.ADD_PRICE = tmp.ADD_PRICE;
            old.AMOUNT = tmp.AMOUNT;
            old.BLOOD_ID = tmp.BLOOD_ID;
            old.DISCOUNT = tmp.DISCOUNT;
            old.EKIP_ID = tmp.EKIP_ID;
            old.EXECUTE_TIME = tmp.EXECUTE_TIME;
            old.HEIN_APPROVAL_ID = tmp.HEIN_APPROVAL_ID;
            old.HEIN_CARD_NUMBER = tmp.HEIN_CARD_NUMBER;
            old.HEIN_LIMIT_PRICE = tmp.HEIN_LIMIT_PRICE;
            old.HEIN_LIMIT_RATIO = tmp.HEIN_LIMIT_RATIO;
            old.HEIN_NORMAL_PRICE = tmp.HEIN_NORMAL_PRICE;
            old.HEIN_PRICE = tmp.HEIN_PRICE;
            old.HEIN_RATIO = tmp.HEIN_RATIO;
            old.INVOICE_ID = tmp.INVOICE_ID;
            old.IS_EXPEND = tmp.IS_EXPEND;
            old.IS_NO_EXECUTE = tmp.IS_NO_EXECUTE;
            old.IS_NO_PAY = tmp.IS_NO_PAY;
            old.IS_OUT_PARENT_FEE = tmp.IS_OUT_PARENT_FEE;
            old.IS_SPECIMEN = tmp.IS_SPECIMEN;
            old.JSON_PATIENT_TYPE_ALTER = tmp.JSON_PATIENT_TYPE_ALTER;
            old.MATERIAL_ID = tmp.MATERIAL_ID;
            old.MEDICINE_ID = tmp.MEDICINE_ID;
            old.ORIGINAL_PRICE = tmp.ORIGINAL_PRICE;
            old.CONFIG_HEIN_LIMIT_PRICE = tmp.CONFIG_HEIN_LIMIT_PRICE;
            old.PARENT_ID = tmp.PARENT_ID;
            old.PATIENT_TYPE_ID = tmp.PATIENT_TYPE_ID;
            old.PARENT_ID = tmp.PARENT_ID;
            old.PRICE = tmp.PRICE;
            old.SERVICE_ID = tmp.SERVICE_ID;
            old.SERVICE_REQ_ID = tmp.SERVICE_REQ_ID;
            old.VAT_RATIO = tmp.VAT_RATIO;
            old.OVERTIME_PRICE = tmp.OVERTIME_PRICE;
            old.IS_ADDITION = tmp.IS_ADDITION;
            old.IS_DELETE = tmp.IS_DELETE;
            old.IS_ACTIVE = tmp.IS_ACTIVE;
            old.TDL_INTRUCTION_TIME = tmp.TDL_INTRUCTION_TIME;
            old.TDL_INTRUCTION_DATE = tmp.TDL_INTRUCTION_DATE;
            old.TDL_TREATMENT_ID = tmp.TDL_TREATMENT_ID;
            old.TDL_SERVICE_REQ_TYPE_ID = tmp.TDL_SERVICE_REQ_TYPE_ID;
            old.TDL_PACS_TYPE_CODE = tmp.TDL_PACS_TYPE_CODE;
            old.TDL_IS_MAIN_EXAM = tmp.TDL_IS_MAIN_EXAM;
            old.TDL_HST_BHYT_CODE = tmp.TDL_HST_BHYT_CODE;
            old.EQUIPMENT_SET_ID = tmp.EQUIPMENT_SET_ID;
            old.EQUIPMENT_SET_ORDER = tmp.EQUIPMENT_SET_ORDER;
            old.PRIMARY_PATIENT_TYPE_ID = tmp.PRIMARY_PATIENT_TYPE_ID;
            old.PRIMARY_PRICE = tmp.PRIMARY_PRICE;
            old.LIMIT_PRICE = tmp.LIMIT_PRICE;
            old.TDL_BILL_OPTION = tmp.TDL_BILL_OPTION;
            old.EXPEND_TYPE_ID = tmp.EXPEND_TYPE_ID;
            old.PATIENT_PRICE_BHYT = tmp.PATIENT_PRICE_BHYT;
            old.OTHER_SOURCE_PRICE = tmp.OTHER_SOURCE_PRICE;
            old.IS_FUND_ACCEPTED = tmp.IS_FUND_ACCEPTED;
			old.TDL_SERVICE_DESCRIPTION = tmp.TDL_SERVICE_DESCRIPTION;
        }
    }
}
