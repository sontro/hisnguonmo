using SAR.MANAGER.Core.SarReportCalendar.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportCalendar
{
    partial class SarReportCalendarUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportCalendarUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReportCalendar.Contains(entity.GetType()))
                {
                    ISarReportCalendarUpdate behavior = SarReportCalendarUpdateBehaviorFactory.MakeISarReportCalendarUpdate(param, entity);
                    result = behavior != null ? behavior.Run() : false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
