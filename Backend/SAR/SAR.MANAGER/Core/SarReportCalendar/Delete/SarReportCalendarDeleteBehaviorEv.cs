using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportCalendar.Delete
{
    class SarReportCalendarDeleteBehaviorEv : BeanObjectBase, ISarReportCalendarDelete
    {
        SAR_REPORT_CALENDAR entity;

        internal SarReportCalendarDeleteBehaviorEv(CommonParam param, SAR_REPORT_CALENDAR data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportCalendarDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportCalendarDAO.Truncate(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarReportCalendarCheckVerifyIsUnlock.Verify(param, entity.ID);
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
