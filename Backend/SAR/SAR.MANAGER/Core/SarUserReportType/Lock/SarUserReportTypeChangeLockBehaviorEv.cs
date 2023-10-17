using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarUserReportType;

namespace SAR.MANAGER.Core.SarUserReportType.Lock
{
    class SarUserReportTypeChangeLockBehaviorEv : BeanObjectBase, ISarUserReportTypeChangeLock
    {
        SAR_USER_REPORT_TYPE entity;

        internal SarUserReportTypeChangeLockBehaviorEv(CommonParam param, SAR_USER_REPORT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarUserReportTypeChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_USER_REPORT_TYPE raw = new SarUserReportTypeBO().Get<SAR_USER_REPORT_TYPE>(entity.ID);
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
                    result = DAOWorker.SarUserReportTypeDAO.Update(raw);
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
