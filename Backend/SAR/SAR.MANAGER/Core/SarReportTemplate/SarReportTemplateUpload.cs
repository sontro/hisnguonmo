using SAR.MANAGER.Core.SarReportTemplate.Upload;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTemplate
{
    partial class SarReportTemplateUpload : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarReportTemplateUpload(CommonParam param, object data)
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
                    ISarReportTemplateUpload behavior = SarReportTemplateUploadBehaviorFactory.MakeISarReportTemplateUpload(param, entity);
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
