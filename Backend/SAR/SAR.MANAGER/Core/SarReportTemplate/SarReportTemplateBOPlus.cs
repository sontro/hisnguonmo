using System;

namespace SAR.MANAGER.Core.SarReportTemplate
{
    partial class SarReportTemplateBO : BusinessObjectBase
    {
        internal bool Upload(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new SarReportTemplateUpload(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal string Download(object data)
        {
            string result = "";
            try
            {
                IDelegacyReportTemplate delegacy = new SarReportTemplateDownload(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = "";
            }
            return result;
        }
    }
}
