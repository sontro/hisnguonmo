using SAR.MANAGER.Core.SarReport.UpdateNameDescription;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReport
{
    partial class SarReportUpdateNameDescription : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportUpdateNameDescription(CommonParam param, object data)
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
                    ISarReportUpdateNameDescription behavior = SarReportUpdateNameDescriptionBehaviorFactory.MakeISarReportUpdateNameDescription(param, entity);
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
