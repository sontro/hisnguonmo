using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.SmsSubclinicalResultNotifyThread
{
    class PrepareDataProcessor
    {
        public static List<SubclinicalResultNotifySmsData> SubclinicalData()
        {
            List<SubclinicalResultNotifySmsData> data = null;
            try
            {
                long dateFrom = Convert.ToInt64(DateTime.Now.AddDays(-SmsCFG.SUBCLINICAL_RESULT_NOTIFY_DAY_NUM).ToString("yyyyMMdd") + "000000");

                //Lay cac du lieu can gui sms
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.IS_INFORM_RESULT_BY_SMS = false; //lay cac y lenh chua gui SMS
                filter.HAS_EXECUTE = true;

                if (SmsCFG.SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_IDS != null && SmsCFG.SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_IDS.Count > 0)
                {
                    filter.REQUEST_ROOM_IDs = SmsCFG.SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_IDS; //Neu ko khai bao thi lay toan bo
                }
                
                filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT; //chi lay cac y lenh da hoan thanh
                filter.SERVICE_REQ_TYPE_IDs = HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs; //Lay cac loai la CLS
                filter.INTRUCTION_DATE_FROM = dateFrom;//luon truyen vao dieu kien nay de toi uu cau lenh truy van (vi bang nay co danh index theo INTRUCTION_DATE)
                filter.INTRUCTION_TIME_FROM = SmsCFG.SUBCLINICAL_RESULT_NOTIFY_START_TIME;
                filter.TDL_TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM; //Chi lay dien dieu tri la kham

                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);

                if (serviceReqs != null && serviceReqs.Count > 0)
                {
                    data = serviceReqs
                        .Where(o => o.TDL_PATIENT_PHONE != null) //chi lay cac BN co khai bao so dien thoai
                        .Select(o => new SubclinicalResultNotifySmsData
                        {
                            ServiceReqId = o.ID,
                            PatientName = o.TDL_PATIENT_NAME,
                            DateOfBirth = o.TDL_PATIENT_DOB,
                            Mobile = o.TDL_PATIENT_PHONE,
                            ServiceReqTypeName = ServiceReqTypeName(o.SERVICE_REQ_TYPE_ID),
                            ExecuteRoomName = ExecuteRoomName(o.EXECUTE_ROOM_ID),
                            ResultingOrder = o.RESULTING_ORDER,
                            ServiceReqTypeId = o.SERVICE_REQ_TYPE_ID
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return data;
        }

        public static List<SubclinicalResultNotifySmsData> ExamData()
        {
            List<SubclinicalResultNotifySmsData> data = null;
            try
            {
                long dateFrom = Convert.ToInt64(DateTime.Now.AddDays(-SmsCFG.SUBCLINICAL_RESULT_NOTIFY_DAY_NUM).ToString("yyyyMMdd") + "000000");

                //Lay cac du lieu can gui sms
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.IS_INFORM_RESULT_BY_SMS = false; //lay cac y lenh chua gui SMS
                filter.HAS_EXECUTE = true;
                if (SmsCFG.SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_IDS != null && SmsCFG.SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_IDS.Count > 0)
                {
                    filter.REQUEST_ROOM_IDs = SmsCFG.SUBCLINICAL_RESULT_NOTIFY_REQUEST_ROOM_IDS; //Neu ko khai bao thi lay toan bo
                }
                filter.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL; //chi lay cac yc kham dang xu ly
                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH; //Lay cac loai la Kham
                filter.INTRUCTION_DATE_FROM = dateFrom;//luon truyen vao dieu kien nay de toi uu cau lenh truy van (vi bang nay co danh index theo INTRUCTION_DATE)
                filter.INTRUCTION_TIME_FROM = SmsCFG.SUBCLINICAL_RESULT_NOTIFY_START_TIME;
                filter.TDL_TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM; //Chi lay dien dieu tri la kham
                filter.HAS_RESULTING_ORDER = true;

                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().Get(filter);

                string serviceReqTypeName = ServiceReqTypeName(IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH);

                if (serviceReqs != null && serviceReqs.Count > 0)
                {
                    data = serviceReqs
                        .Where(o => o.TDL_PATIENT_PHONE != null) //chi lay cac BN co khai bao so dien thoai
                        .Select(o => new SubclinicalResultNotifySmsData
                        {
                            ServiceReqId = o.ID,
                            PatientName = o.TDL_PATIENT_NAME,
                            DateOfBirth = o.TDL_PATIENT_DOB,
                            Mobile = o.TDL_PATIENT_PHONE,
                            ServiceReqTypeName = serviceReqTypeName,
                            ExecuteRoomName = ExecuteRoomName(o.EXECUTE_ROOM_ID),
                            ResultingOrder = o.RESULTING_ORDER,
                            ServiceReqTypeId = o.SERVICE_REQ_TYPE_ID
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return data;
        }

        private static string ServiceReqTypeName(long id)
        {
            HIS_SERVICE_REQ_TYPE type = HisServiceReqTypeCFG.DATA.Where(o => o.ID == id).FirstOrDefault();
            return type != null ? type.SERVICE_REQ_TYPE_NAME : null;
        }

        private static string ExecuteRoomName(long id)
        {
            V_HIS_EXECUTE_ROOM room = HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == id).FirstOrDefault();
            return room != null ? room.EXECUTE_ROOM_NAME : null;
        }
    }
}
