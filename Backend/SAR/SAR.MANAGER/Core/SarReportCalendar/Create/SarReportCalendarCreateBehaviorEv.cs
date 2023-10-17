using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportCalendar.Create
{
    class SarReportCalendarCreateBehaviorEv : BeanObjectBase, ISarReportCalendarCreate
    {
        SAR_REPORT_CALENDAR entity;

        internal SarReportCalendarCreateBehaviorEv(CommonParam param, SAR_REPORT_CALENDAR data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportCalendarCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportCalendarDAO.Create(entity);
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
                result = result && SarReportCalendarCheckVerifyValidData.Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
