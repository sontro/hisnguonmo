using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarReportTypeGroup;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Lock
{
    class SarReportTypeGroupChangeLockBehaviorEv : BeanObjectBase, ISarReportTypeGroupChangeLock
    {
        SAR_REPORT_TYPE_GROUP entity;

        internal SarReportTypeGroupChangeLockBehaviorEv(CommonParam param, SAR_REPORT_TYPE_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTypeGroupChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_REPORT_TYPE_GROUP raw = new SarReportTypeGroupBO().Get<SAR_REPORT_TYPE_GROUP>(entity.ID);
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
                    result = DAOWorker.SarReportTypeGroupDAO.Update(raw);
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
