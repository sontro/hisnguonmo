using Inventec.Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Browser
{
    class BrowserJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("Begin BrowserJob.");

                //Job gọi vào các backend không có api để duy trì hoạt động
                string browserUri = ConfigurationManager.AppSettings["Browser.uri.base"];
                if (!String.IsNullOrWhiteSpace(browserUri))
                {
                    string[] uris = browserUri.Split('|');
                    foreach (var item in uris)
                    {
                        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)(System.Net.WebRequest.Create(item));
                        request.Method = "GET";
                        request.KeepAlive = true;
                        request.Timeout = 90000;

                        using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)(request.GetResponse()))
                        {
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => response), response));
                        }
                    }
                }
                LogSystem.Info("End BrowserJob.");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
