using SAR.MANAGER.Core.SarReportTemplate.Download;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTemplate
{
    partial class SarReportTemplateDownload : BeanObjectBase, IDelegacyReportTemplate
    {
        object entity;

        internal SarReportTemplateDownload(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        string IDelegacyReportTemplate.Execute()
        {
            string result = "";
            try
            {
                if (TypeCollection.SarReportTemplate.Contains(entity.GetType()))
                {
                    ISarReportTemplateDownload behavior = SarReportTemplateDownloadBehaviorFactory.MakeISarReportTemplateDownload(param, entity);
                    result = behavior != null ? behavior.Run() : String.Empty;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = String.Empty;
            }
            return result;
        }
    }
}
