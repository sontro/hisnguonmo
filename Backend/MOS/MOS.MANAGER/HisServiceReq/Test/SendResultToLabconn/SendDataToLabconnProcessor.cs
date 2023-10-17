using MOS.TDO;

using Inventec.Common.Logging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MOS.MANAGER.HisServiceReq.Test.SendResultToLabconn
{
    public class SendDataToLabconnProcessor
    {
        public bool SendToLabconn(LisLabconnSenderTDO tdo, string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    LogSystem.Info("uriLabconn__:" + uri);
                    var response = client.PostAsJsonAsync<LisLabconnSenderTDO>("LisReceiver/web/SavePatientSequence", tdo).Result;
                    LogSystem.Info(LogUtil.TraceData("response from lislabconn__:", response));
                    if (response.IsSuccessStatusCode)
                    {
                        var successResponse = response.Content.ReadAsStringAsync().Result;
                        LabconnResponseTDO resTDO = JsonConvert.DeserializeObject<LabconnResponseTDO>(successResponse);
                        LogSystem.Info(LogUtil.TraceData("response from lislabconn__:", response.Content.ReadAsStringAsync().Result));
                        return (resTDO != null && resTDO.IsSuccess && resTDO.ErrorMessage == null);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return false;
        }

        public bool SendPatiInfoToLabconn(UpdatePatientInfoTDO tdo, string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    LogSystem.Info("uriLabconn__:" + uri);
                    var response = client.PostAsJsonAsync<UpdatePatientInfoTDO>("LisReceiver/web/UpdatePatientInfo", tdo).Result;
                    LogSystem.Info(LogUtil.TraceData("response from lislabconn__:", response));
                    if (response.IsSuccessStatusCode)
                    {
                        var successResponse = response.Content.ReadAsStringAsync().Result;
                        LabconnResponseTDO resTDO = JsonConvert.DeserializeObject<LabconnResponseTDO>(successResponse);
                        LogSystem.Info(LogUtil.TraceData("response from lislabconn__:", response.Content.ReadAsStringAsync().Result));
                        return (resTDO != null && resTDO.IsSuccess && resTDO.ErrorMessage == null);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return false;
        }
    }
}
