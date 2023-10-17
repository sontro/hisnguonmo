using SAR.MANAGER.Core.SarReportCalendar.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportCalendar
{
    partial class SarReportCalendarCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportCalendarCreate(CommonParam param, object data)
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
                    ISarReportCalendarCreate behavior = SarReportCalendarCreateBehaviorFactory.MakeISarReportCalendarCreate(param, entity);
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
