using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using SAR.SDO;
using System.Collections.Generic;
using Inventec.Fss.Utility;
using Inventec.Fss.Client;
using AutoMapper;
using System.Configuration;

namespace SAR.MANAGER.Core.SarReportTemplate.Download
{
    class SarReportTemplateDownloadBehaviorSDO : BeanObjectBase, ISarReportTemplateDownload
    {
        long entity;

        internal SarReportTemplateDownloadBehaviorSDO(CommonParam param, long data)
            : base(param)
        {
            entity = data;
        }

        string ISarReportTemplateDownload.Run()
        {
            string result = "";
            try
            {
                if (Check())
                {
                    var reportTemplate = new SarReportTemplateBO().Get<SAR.EFMODEL.DataModels.SAR_REPORT_TEMPLATE>(entity);
                    if (reportTemplate != null)
                    {
                        dynamic urlData = Newtonsoft.Json.JsonConvert.DeserializeObject(reportTemplate.REPORT_TEMPLATE_URL);
                        if (urlData != null)
                        {
                            result = ConfigurationManager.AppSettings["fss.uri.base"] + "" + urlData.URL;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = "";
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarReportTemplateCheckVerifyIsUnlock.Verify(param, entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
