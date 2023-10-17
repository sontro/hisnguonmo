using SAR.MANAGER.Core.SarReportTemplate.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTemplate
{
    partial class SarReportTemplateDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTemplateDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarReportTemplate.Contains(entity.GetType()))
                {
                    ISarReportTemplateDelete behavior = SarReportTemplateDeleteBehaviorFactory.MakeISarReportTemplateDelete(param, entity);
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
