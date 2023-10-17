using SAR.MANAGER.Core.SarReportCalendar.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportCalendar
{
    partial class SarReportCalendarDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportCalendarDelete(CommonParam param, object data)
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
                    ISarReportCalendarDelete behavior = SarReportCalendarDeleteBehaviorFactory.MakeISarReportCalendarDelete(param, entity);
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
