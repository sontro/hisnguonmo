using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportCalendar.Create
{
    class SarReportCalendarCreateBehaviorListEv : BeanObjectBase, ISarReportCalendarCreate
    {
        List<SAR_REPORT_CALENDAR> entities;

        internal SarReportCalendarCreateBehaviorListEv(CommonParam param, List<SAR_REPORT_CALENDAR> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISarReportCalendarCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportCalendarDAO.CreateList(entities);
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
                result = result && SarReportCalendarCheckVerifyValidData.Verify(param, entities);
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
