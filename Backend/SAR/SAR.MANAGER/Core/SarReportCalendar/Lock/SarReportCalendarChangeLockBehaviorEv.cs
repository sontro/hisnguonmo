using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using SAR.MANAGER.Core.SarReportCalendar;

namespace SAR.MANAGER.Core.SarReportCalendar.Lock
{
    class SarReportCalendarChangeLockBehaviorEv : BeanObjectBase, ISarReportCalendarChangeLock
    {
        SAR_REPORT_CALENDAR entity;

        internal SarReportCalendarChangeLockBehaviorEv(CommonParam param, SAR_REPORT_CALENDAR data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportCalendarChangeLock.Run()
        {
            bool result = false;
            try
            {
                SAR_REPORT_CALENDAR raw = new SarReportCalendarBO().Get<SAR_REPORT_CALENDAR>(entity.ID);
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
                    result = DAOWorker.SarReportCalendarDAO.Update(raw);
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
