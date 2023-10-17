using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarReport;

namespace SAR.MANAGER.Core.SarReport.Lock
{
    class SarReportChangeLockBehaviorEv : BeanObjectBase, ISarReportChangeLock
    {
        SAR_REPORT entity;

        internal SarReportChangeLockBehaviorEv(CommonParam param, SAR_REPORT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_REPORT raw = new SarReportBO().Get<SAR_REPORT>(entity.ID);
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
                    result = DAOWorker.SarReportDAO.Update(raw);
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
