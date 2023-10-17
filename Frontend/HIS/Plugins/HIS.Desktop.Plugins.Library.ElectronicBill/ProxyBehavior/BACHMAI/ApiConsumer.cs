using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.BACHMAI
{
    class ApiConsumer
    {
        public static T CreateRequest<T>(string baseUri, string token, string requestUri, string tid, object objData)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                if (!String.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                }

                client.Timeout = new TimeSpan(0, 0, 90);

                HttpResponseMessage resp = null;
                string sendJsonData = null;
                if (!String.IsNullOrWhiteSpace(tid))
                {
                    SendDataADO sendData = new SendDataADO();
                    sendData.tid = tid;
                    sendData.data = objData;
                    sendJsonData = JsonConvert.SerializeObject(sendData);
                }
                else if (!String.IsNullOrWhiteSpace(requestUri))
                {
                    sendJsonData = JsonConvert.SerializeObject(objData);
                }

                Inventec.Common.Logging.LogSystem.Info("_____sendJsonData : " + sendJsonData);

                string fullrequestUri = requestUri;
                int index = baseUri.IndexOf('/', baseUri.IndexOf("//") + 2);
                if (index > 0)
                {
                    string extension = baseUri.Substring(index);
                    if (!requestUri.Contains(extension))
                    {
                        fullrequestUri = extension + requestUri;
                    }
                }

                resp = client.PostAsync(fullrequestUri, new StringContent(sendJsonData, Encoding.UTF8, "application/json")).Result;

                if (resp == null || !resp.IsSuccessStatusCode)
                {
                    int statusCode = resp.StatusCode.GetHashCode();
                    throw new Exception(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", baseUri, requestUri, statusCode));
                }

                string responseData = resp.Content.ReadAsStringAsync().Result;
                Inventec.Common.Logging.LogSystem.Info("__________________api responseData: " + responseData);
                T data = default(T);
                try
                {
                    data = JsonConvert.DeserializeObject<T>(responseData);
                    if (data == null)
                    {
                        throw new Exception(string.Format("Loi khi goi API. Response {0}:", responseData));
                    }
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                    throw new Exception(responseData);
                }
                return data;
            }
        }
    }
}
