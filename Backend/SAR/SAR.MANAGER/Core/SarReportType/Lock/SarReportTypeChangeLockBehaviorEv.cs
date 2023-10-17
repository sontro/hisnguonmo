using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarReportType;

namespace SAR.MANAGER.Core.SarReportType.Lock
{
    class SarReportTypeChangeLockBehaviorEv : BeanObjectBase, ISarReportTypeChangeLock
    {
        SAR_REPORT_TYPE entity;

        internal SarReportTypeChangeLockBehaviorEv(CommonParam param, SAR_REPORT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTypeChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_REPORT_TYPE raw = new SarReportTypeBO().Get<SAR_REPORT_TYPE>(entity.ID);
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
                    result = DAOWorker.SarReportTypeDAO.Update(raw);
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
