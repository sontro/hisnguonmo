using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportCalendar.Create
{
    class SarReportCalendarCreateBehaviorFactory
    {
        internal static ISarReportCalendarCreate MakeISarReportCalendarCreate(CommonParam param, object data)
        {
            ISarReportCalendarCreate result = null;
            try
            {
                if (data.GetType() == typeof(SAR_REPORT_CALENDAR))
                {
                    result = new SarReportCalendarCreateBehaviorEv(param, (SAR_REPORT_CALENDAR)data);
                }
                else if (data.GetType() == typeof(List<SAR_REPORT_CALENDAR>))
                {
                    result = new SarReportCalendarCreateBehaviorListEv(param, (List<SAR_REPORT_CALENDAR>)data);
                }
                if (result == null) throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__FactoryKhoiTaoDoiTuongThatBai);
                Inventec.Common.Logging.LogSystem.Error("Factory khong khoi tao duoc doi tuong." + data.GetType().ToString() + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data), ex);
                result = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
