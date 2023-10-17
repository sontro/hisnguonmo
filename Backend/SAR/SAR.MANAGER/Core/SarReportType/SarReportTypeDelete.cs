using SAR.MANAGER.Core.SarReportType.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType
{
    partial class SarReportTypeDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTypeDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReportType.Contains(entity.GetType()))
                {
                    ISarReportTypeDelete behavior = SarReportTypeDeleteBehaviorFactory.MakeISarReportTypeDelete(param, entity);
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
