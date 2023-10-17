using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportCalendar.Update
{
    class SarReportCalendarUpdateBehaviorEv : BeanObjectBase, ISarReportCalendarUpdate
    {
        SAR_REPORT_CALENDAR entity;

        internal SarReportCalendarUpdateBehaviorEv(CommonParam param, SAR_REPORT_CALENDAR data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportCalendarUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportCalendarDAO.Update(entity);
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
                result = result && SarReportCalendarCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SarReportCalendarCheckVerifyExistsCode.Verify(param, entity.REPORT_CALENDAR_CODE, entity.ID);
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
