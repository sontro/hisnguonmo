using SAR.MANAGER.Core.SarReportTemplate;
using Inventec.Core;
using System;
using AutoMapper;
using SAR.SDO;

namespace SAR.MANAGER.Manager
{
    public partial class SarReportTemplateManager : ManagerBase
    {
        public object Upload(object data)
        {
            object result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportTemplateBO bo = new SarReportTemplateBO();
                if (bo.Upload(data))
                {
                    Mapper.CreateMap<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE, SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>();
                    result = Mapper.Map<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE, SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>((SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE)data);
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        public object Download(object data)
        {
            SarReportTemplateDownloadSDO result = null;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                SarReportTemplateBO bo = new SarReportTemplateBO();
                string resultUrl = bo.Download(data);
                if (!String.IsNullOrEmpty(resultUrl))
                {
                    result = new SarReportTemplateDownloadSDO();
                    result.ReportTemplateId = (long)data;
                    result.UrlResult = resultUrl;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
