using SAR.MANAGER.Core.SarReportTemplate.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTemplate
{
    partial class SarReportTemplateUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTemplateUpdate(CommonParam param, object data)
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
                    ISarReportTemplateUpdate behavior = SarReportTemplateUpdateBehaviorFactory.MakeISarReportTemplateUpdate(param, entity);
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
