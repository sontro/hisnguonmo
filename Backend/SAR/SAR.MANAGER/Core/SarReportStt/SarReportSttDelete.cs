using SAR.MANAGER.Core.SarReportStt.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt
{
    partial class SarReportSttDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportSttDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReportStt.Contains(entity.GetType()))
                {
                    ISarReportSttDelete behavior = SarReportSttDeleteBehaviorFactory.MakeISarReportSttDelete(param, entity);
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
