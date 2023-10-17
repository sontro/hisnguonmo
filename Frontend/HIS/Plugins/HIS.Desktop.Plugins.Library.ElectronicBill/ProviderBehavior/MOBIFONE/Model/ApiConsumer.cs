using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProviderBehavior.MOBIFONE.Model
{
    public class ApiConsumer
    {
        public static T CreateRequest<T>(string method, string baseUri, string requestUri, string token, string maDvcs, string sendJsonData = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                if (!String.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bear {0};{1}", token, maDvcs));
                }

                client.Timeout = new TimeSpan(0, 0, 90);

                HttpResponseMessage resp = null;

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
                Inventec.Common.Logging.LogSystem.Info(string.Format("API: {0}. sendJsonData: {1}", fullrequestUri, sendJsonData));
                if (method.Equals(System.Net.WebRequestMethods.Http.Get))
                    resp = client.GetAsync(fullrequestUri).Result;
                else
                    resp = client.PostAsync(fullrequestUri, new StringContent(sendJsonData, Encoding.UTF8, "application/json")).Result;

                if (resp == null || !resp.IsSuccessStatusCode)
                {
                    int statusCode = resp.StatusCode.GetHashCode();
                    throw new Exception(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", baseUri, requestUri, statusCode));
                }
                T data = default(T);
                string responseData = resp.Content.ReadAsStringAsync().Result;
                Inventec.Common.Logging.LogSystem.Info("__________________api responseData: " + responseData);
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
        public static byte[] CreateRequestGetByte(string method, string baseUri, string requestUri, string token, string maDvcs)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                if (!String.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bear {0};{1}", token, maDvcs));
                }

                client.Timeout = new TimeSpan(0, 0, 90);

                HttpResponseMessage resp = null;

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

                if (method.Equals(System.Net.WebRequestMethods.Http.Get))
                    resp = client.GetAsync(fullrequestUri).Result;              
                if (resp == null || !resp.IsSuccessStatusCode)
                {
                    int statusCode = resp.StatusCode.GetHashCode();
                    throw new Exception(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", baseUri, requestUri, statusCode));
                }
                
                byte[] responseData = resp.Content.ReadAsByteArrayAsync().Result;
                return responseData;
            }
        }
    }
}
