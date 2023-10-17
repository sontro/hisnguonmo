using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Util
{
    class HisTreatmentNotify : BusinessBase
    {
        internal HisTreatmentNotify()
            : base()
        {

        }

        internal HisTreatmentNotify(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(long treatmentId)
        {
            bool result = false;
            try
            {
                if (!HisTreatmentCFG.NOTIFY_APPROVE_MEDI_RECORD_WHEN_TREATMENT_FINISH)
                {
                    return true;
                }
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(treatmentId);

                if (treatment == null || treatment.IS_PAUSE != Constant.IS_TRUE || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    LogSystem.Info("HisTreatmentNotify. TreatmentId invalid or Treatment is not pause or TreatmentTypeId is exam.\n" + LogUtil.TraceData("treatment", treatment));
                    return false;
                }

                List<string> loginnames = HisUserRoomCFG.DATA.Where(o => o.IS_ACTIVE == Constant.IS_TRUE && HisRoomCFG.DATA.Any(a => a.ID == o.ROOM_ID && a.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__LT && a.IS_ACTIVE == Constant.IS_TRUE)).Select(s => s.LOGINNAME).Distinct().ToList();

                if (IsNotNullOrEmpty(loginnames))
                {
                    List<SDA_NOTIFY> sdaNotifys = new List<SDA_NOTIFY>();
                    foreach (string loginname in loginnames)
                    {
                        SDA_NOTIFY notify = new SDA_NOTIFY();
                        notify.CONTENT = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_NoiDungThongBaoDuyetHoSoBenhAn, param.LanguageCode), treatment.TDL_PATIENT_NAME, treatment.TREATMENT_CODE);
                        notify.FROM_TIME = Inventec.Common.DateTime.Get.Now().Value;
                        notify.RECEIVER_LOGINNAME = loginname;
                        notify.TITLE = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_TieuDeThongBaoDuyetHoSoBenhAn, param.LanguageCode), treatment.TREATMENT_CODE);
                        sdaNotifys.Add(notify);
                    }
                    var rs = ApiConsumerManager.ApiConsumerStore.SdaConsumerWrapper.Post<List<SDA_NOTIFY>>(true, "api/SdaNotify/CreateList", param, sdaNotifys);
                    if (rs == null || rs.Count <= 0)
                    {
                        LogSystem.Warn("HisTreatmentNotify. Send Create SdaNotify Failed.\n" + LogUtil.TraceData("Param", param));
                    }
                    else
                    {
                        LogSystem.Debug("HisTreatmentNotify. Send Create SdaNotify Success.");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool RunRejectStore(long treatmentId)
        {
            bool result = false;
            try
            {
                if (!HisTreatmentCFG.NOTIFY_APPROVE_MEDI_RECORD_WHEN_TREATMENT_FINISH)
                {
                    return true;
                }
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(treatmentId);

                if (treatment == null || treatment.APPROVAL_STORE_STT_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT.APPROVAL_STORE_STT_ID__TU_CHOI || String.IsNullOrWhiteSpace(treatment.END_LOGINNAME))
                {
                    LogSystem.Info("HisTreatmentNotify. TreatmentId invalid or Treatment is not reject store or EndLoginname is null.\n" + LogUtil.TraceData("treatment", treatment));
                    return false;
                }

                List<SDA_NOTIFY> sdaNotifys = new List<SDA_NOTIFY>();

                SDA_NOTIFY notify = new SDA_NOTIFY();
                notify.CONTENT = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_NoiDungThongBaoTuChoiDuyetHoSoBenhAn, param.LanguageCode), treatment.TDL_PATIENT_NAME, treatment.TREATMENT_CODE);
                notify.FROM_TIME = Inventec.Common.DateTime.Get.Now().Value;
                notify.RECEIVER_LOGINNAME = treatment.END_LOGINNAME;
                notify.TITLE = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_TieuDeThongBaoTuChoiDuyetHoSoBenhAn, param.LanguageCode), treatment.TREATMENT_CODE);
                sdaNotifys.Add(notify);
                var rs = ApiConsumerManager.ApiConsumerStore.SdaConsumerWrapper.Post<List<SDA_NOTIFY>>(true, "api/SdaNotify/CreateList", param, sdaNotifys);
                if (rs == null || rs.Count <= 0)
                {
                    LogSystem.Warn("HisTreatmentNotify. Send Create SdaNotify Failed.\n" + LogUtil.TraceData("Param", param));
                }
                else
                {
                    LogSystem.Debug("HisTreatmentNotify. Send Create SdaNotify Success.");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool RunHandledRejectStore(long treatmentId)
        {
            bool result = false;
            try
            {
                if (!HisTreatmentCFG.NOTIFY_APPROVE_MEDI_RECORD_WHEN_TREATMENT_FINISH)
                {
                    return true;
                }
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(treatmentId);

                if (treatment == null || treatment.IS_PAUSE != Constant.IS_TRUE || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    LogSystem.Info("HisTreatmentNotify. TreatmentId invalid or Treatment is not pause or TreatmentTypeId is exam.\n" + LogUtil.TraceData("treatment", treatment));
                    return false;
                }

                List<string> loginnames = HisUserRoomCFG.DATA.Where(o => o.IS_ACTIVE == Constant.IS_TRUE && HisRoomCFG.DATA.Any(a => a.ID == o.ROOM_ID && a.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__LT && a.IS_ACTIVE == Constant.IS_TRUE)).Select(s => s.LOGINNAME).Distinct().ToList();

                if (IsNotNullOrEmpty(loginnames))
                {
                    List<SDA_NOTIFY> sdaNotifys = new List<SDA_NOTIFY>();
                    foreach (string loginname in loginnames)
                    {
                        SDA_NOTIFY notify = new SDA_NOTIFY();
                        notify.CONTENT = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_NoiDungThongBaoDuyetHoSoBenhAnDaHoanThanhLai, param.LanguageCode), treatment.TDL_PATIENT_NAME, treatment.TREATMENT_CODE);
                        notify.FROM_TIME = Inventec.Common.DateTime.Get.Now().Value;
                        notify.RECEIVER_LOGINNAME = loginname;
                        notify.TITLE = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisTreatment_TieuDeThongBaoDuyetHoSoBenhAn, param.LanguageCode), treatment.TREATMENT_CODE);
                        sdaNotifys.Add(notify);
                    }
                    var rs = ApiConsumerManager.ApiConsumerStore.SdaConsumerWrapper.Post<List<SDA_NOTIFY>>(true, "api/SdaNotify/CreateList", param, sdaNotifys);
                    if (rs == null || rs.Count <= 0)
                    {
                        LogSystem.Warn("HisTreatmentNotify. Send Create SdaNotify Failed.\n" + LogUtil.TraceData("Param", param));
                    }
                    else
                    {
                        LogSystem.Debug("HisTreatmentNotify. Send Create SdaNotify Success.");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

    }
}
