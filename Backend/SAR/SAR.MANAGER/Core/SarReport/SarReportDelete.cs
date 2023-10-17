using SAR.MANAGER.Core.SarReport.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReport
{
    partial class SarReportDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReport.Contains(entity.GetType()))
                {
                    ISarReportDelete behavior = SarReportDeleteBehaviorFactory.MakeISarReportDelete(param, entity);
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
