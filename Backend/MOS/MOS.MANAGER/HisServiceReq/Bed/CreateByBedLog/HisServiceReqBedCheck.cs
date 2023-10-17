using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Bed.CreateByBedLog
{
    class HisServiceReqBedCheck : HisServiceReqCheck
    {
        internal HisServiceReqBedCheck()
            : base()
        {
        }

        internal HisServiceReqBedCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidPatientType(HisBedServiceReqSDO sdo, ref List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters)
        {
            bool valid = true;
            try
            {
                //Kiem tra thong tin dien doi tuong hien tai ho so dieu tri
                patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentId(sdo.TreatmentId);

                if (!IsNotNullOrEmpty(patientTypeAlters))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongTonTaiThongTinDienDoiTuong);
                    return false;
                }

                if (IsNotNullOrEmpty(sdo.BedServices))
                {
                    //Duyet theo thoi gian y lenh de check dien doi tuong
                    foreach (HisBedServiceSDO bs in sdo.BedServices)
                    {
                        //Lay dien doi tuong ap dung
                        HIS_PATIENT_TYPE_ALTER pta = new HisPatientTypeAlterGet().GetApplied(sdo.TreatmentId, bs.InstructionTime, patientTypeAlters);
                        //Lay cac doi tuong thanh toan su dung de kiem tra
                        List<long> patientTypeIds = new List<long>();
                        foreach(ServiceReqDetailSDO s in bs.ServiceReqDetails)
                        {
                            List<long> p = IsNotNullOrEmpty(bs.ServiceReqDetails) ? bs.ServiceReqDetails.Select(o => o.PatientTypeId).ToList() : null;
                            if (IsNotNullOrEmpty(p))
                            {
                                patientTypeIds.AddRange(p);
                            }
                        }
                        //Chi kiem tra doi voi cac doi tuong co "dong chi tra"
                        patientTypeIds = patientTypeIds.Where(o => !HisPatientTypeCFG.NO_CO_PAYMENT.Exists(t => t.ID == o)).Distinct().ToList();

                        //Kiem tra xem trong cac doi tuong thanh toan, co doi tuong thanh toan nao khac voi dien doi tuong cua BN ko
                        List<long> notExistIds = patientTypeIds.Where(o => pta == null || o != pta.PATIENT_TYPE_ID).ToList();
                        if (IsNotNullOrEmpty(notExistIds))
                        {
                            List<string> patientTypeNames = HisPatientTypeCFG.DATA.Where(o => notExistIds.Contains(o.ID)).Select(o => o.PATIENT_TYPE_NAME).ToList();
                            string nameStr = string.Join(",", patientTypeNames);
                            string time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(bs.InstructionTime);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongTonTaiThongTinDienDoiTuongTruocThoiDiemYLenh, nameStr, time);
                            return false;
                        }

                        //Neu dien doi tuong la BHYT thi kiem tra han su dung
                        if (pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && patientTypeIds != null && patientTypeIds.Contains(HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                        {
                            //Cong them "so ngay cho phep vuot qua" truoc khi check han the
                            int exceedDayAllow = pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU || pta.TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY ? HisHeinBhytCFG.EXCEED_DAY_ALLOW_FOR_IN_PATIENT : 0;

                            DateTime toTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(pta.HEIN_CARD_TO_TIME.Value).Value.AddDays(exceedDayAllow);
                            long toTimeNum = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(toTime).Value;

                            long? heinCardFromDate = Inventec.Common.DateTime.Get.StartDay(pta.HEIN_CARD_FROM_TIME.Value);
                            long? heinCardToDate = Inventec.Common.DateTime.Get.StartDay(toTimeNum);
                            long? instructionDate = Inventec.Common.DateTime.Get.StartDay(bs.InstructionTime);

                            if (heinCardFromDate > instructionDate || heinCardToDate < instructionDate)
                            {
                                string instructionTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToDateString(bs.InstructionTime);
                                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ThoiGianYLenhKhongNamTrongKhoangThoiGianHieuLucCuaTheBhyt, instructionTimeStr);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidInstructionTime(HisBedServiceReqSDO sdo, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                List<long> instructionTimes = IsNotNullOrEmpty(sdo.BedServices) ? sdo.BedServices.Select(o => o.InstructionTime).ToList() : null;
                valid = this.IsValidInstructionTime(instructionTimes, treatment);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private string BedLogContent(V_HIS_BED_LOG bedLog)
        {
            try
            {
                if (bedLog != null)
                {
                    string startTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(bedLog.START_TIME);
                    string finishTime = bedLog.FINISH_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(bedLog.FINISH_TIME.Value) : "";
                    return string.Format("[{0}-{1}-({2} --> {3})]", bedLog.BED_CODE, bedLog.BED_NAME, startTime, finishTime);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return "";
        }
    }
}
