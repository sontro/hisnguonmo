using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarReportStt;

namespace SAR.MANAGER.Core.SarReportStt.Lock
{
    class SarReportSttChangeLockBehaviorEv : BeanObjectBase, ISarReportSttChangeLock
    {
        SAR_REPORT_STT entity;

        internal SarReportSttChangeLockBehaviorEv(CommonParam param, SAR_REPORT_STT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportSttChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_REPORT_STT raw = new SarReportSttBO().Get<SAR_REPORT_STT>(entity.ID);
                if (raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.SAR_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.SarReportSttDAO.Update(raw);
                    if (result) entity.IS_ACTIVE = raw.IS_ACTIVE;
                }
                else
                {
                    BugUtil.SetBugCode(param, SAR.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
