using SAR.MANAGER.Core.SarReportTemplate.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTemplate
{
    partial class SarReportTemplateChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTemplateChangeLock(CommonParam param, object data)
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
                    ISarReportTemplateChangeLock behavior = SarReportTemplateChangeLockBehaviorFactory.MakeISarReportTemplateChangeLock(param, entity);
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
