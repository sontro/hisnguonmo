using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using SMS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.SmsSubclinicalResultNotifyThread
{
    /// <summary>
    /// Phuc vu gui SMS thong bao da co ket qua CLS
    /// </summary>
    public class SmsSubclinicalResultNotifyThreadSender : BusinessBase
    {
        private static bool IS_SENDING = false;

        public static void Run()
        {
            try
            {
                if (IS_SENDING)
                {
                    LogSystem.Warn("Tien trinh xu ly nhan sms thong bao ket qua CLS dang chay, khong cho phep khoi tao tien trinh moi");
                    return;
                }

                IS_SENDING = true;

                if (!string.IsNullOrWhiteSpace(SmsCFG.MERCHANT_CODE) && !string.IsNullOrWhiteSpace(SmsCFG.SECURITY_CODE)
                    && SmsCFG.SUBCLINICAL_RESULT_NOTIFY_START_TIME > 0 //nghiep vu nay la nhan tin sms cho BN (gay anh huong BN) nen can bat buoc khai bao thoi diem bat dau chay
                    && (!string.IsNullOrWhiteSpace(SmsCFG.SUBCLINICAL_RESULT_NOTIFY_MESSAGE_FORMAT)
                    || !string.IsNullOrWhiteSpace(SmsCFG.SUBCLINICAL_RESULT_NOTIFY_RESULTING_MESSAGE_FORMAT)
                    )//bat buoc khai bao dinh dang sms 
                    )
                {
                    List<SubclinicalResultNotifySmsData> subclinicalData = null;
                    List<SubclinicalResultNotifySmsData> examData = null;

                    if (!string.IsNullOrWhiteSpace(SmsCFG.SUBCLINICAL_RESULT_NOTIFY_MESSAGE_FORMAT))
                    {
                        subclinicalData = PrepareDataProcessor.SubclinicalData();
                    }
                    if (!string.IsNullOrWhiteSpace(SmsCFG.SUBCLINICAL_RESULT_NOTIFY_RESULTING_MESSAGE_FORMAT))
                    {
                        examData = PrepareDataProcessor.ExamData();
                    }

                    List<long> successList = new List<long>();

                    if (subclinicalData != null && subclinicalData.Count > 0 && !string.IsNullOrWhiteSpace(SmsCFG.SUBCLINICAL_RESULT_NOTIFY_MESSAGE_FORMAT))
                    {
                        //Group de trong truong hop, BN co 2 chi dinh cung loai deu co ket qua thi chi nhan 1 sms
                        var group = subclinicalData.GroupBy(o => new { o.PatientName, o.Mobile, o.ServiceReqTypeName, o.DateOfBirth });

                        foreach (var g in group)
                        {
                            List<long> ids = g.Select(o => o.ServiceReqId).ToList();
                            SendSms(SmsCFG.SUBCLINICAL_RESULT_NOTIFY_MESSAGE_FORMAT, ids, g.Key.Mobile, g.Key.DateOfBirth, g.Key.PatientName, g.Key.ServiceReqTypeName, null, null, ref successList);
                        }
                    }
                    if (examData != null && examData.Count > 0 && !string.IsNullOrWhiteSpace(SmsCFG.SUBCLINICAL_RESULT_NOTIFY_RESULTING_MESSAGE_FORMAT))
                    {
                        foreach (var exam in examData)
                        {
                            SendSms(SmsCFG.SUBCLINICAL_RESULT_NOTIFY_RESULTING_MESSAGE_FORMAT, new List<long>(){exam.ServiceReqId}, exam.Mobile, exam.DateOfBirth, exam.PatientName, exam.ServiceReqTypeName, exam.ExecuteRoomName, exam.ResultingOrder, ref successList);
                        }
                    }

                    if (successList != null && successList.Count > 0 && !new UpdateDataProcessor().Run(successList))
                    {
                        LogSystem.Error("Cap nhat trang thai da nhan SMS thong bao ket qua CLS that bai");
                    }
                }

                IS_SENDING = false;
            }
            catch (Exception ex)
            {
                IS_SENDING = false;
                LogSystem.Error(ex);
            }
        }

        private static void SendSms(string smsFormat, List<long> ids, string mobile, long dateOfBirth, string patientName, string serviceReqTypeName, string executeRoomName, long? resultingOrder, ref List<long> successList)
        {
            string age = Inventec.Common.DateTime.Calculation.AgeCaption(dateOfBirth);//g.Key.DateOfBirth
            string resultingOrderStr = resultingOrder.HasValue ? resultingOrder.ToString() : "";
            executeRoomName = executeRoomName == null ? "" : executeRoomName;

            string content = smsFormat
                .Replace(SmsCFG.RESULT_NOTIFY_MESSAGE_FORMAT_KEY__AGE, age)
                .Replace(SmsCFG.RESULT_NOTIFY_MESSAGE_FORMAT_KEY__PATIENT_NAME, patientName) //g.Key.PatientName
                .Replace(SmsCFG.RESULT_NOTIFY_MESSAGE_FORMAT_KEY__SERVICE_REQ_TYPE_NAME, serviceReqTypeName) //g.Key.ServiceReqTypeName
                .Replace(SmsCFG.RESULT_NOTIFY_MESSAGE_FORMAT_KEY__EXECUTE_ROOM_NAME, executeRoomName)
                .Replace(SmsCFG.RESULT_NOTIFY_MESSAGE_FORMAT_KEY__RESULTING_ORDER, resultingOrderStr)
                ;

            content = Inventec.Common.String.Convert.UnSignVNese(content);
            content = content.ToUpper();

            SmsMtSDO mtSDO = new SmsMtSDO();
            mtSDO.Content = content;
            mtSDO.MerchantCode = SmsCFG.MERCHANT_CODE;
            mtSDO.SecurityCode = SmsCFG.SECURITY_CODE;
            mtSDO.Mobile = mobile;

            var rs = ApiConsumerStore.SmsConsumer.Post<SmsMtSDO>(true, "api/SmsMt/Create", new CommonParam(), mtSDO);
            if (rs != null && rs.ResponseCode == SmsCFG.RESPONSE_CODE__SUCCESS)
            {
                
                if (ids != null && ids.Count > 0)
                {
                    successList.AddRange(ids);
                }
            }
        }
    }
}
