using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.CTO
{
    class ApiConsumer
    {
        public static T CreateRequest<T>(string baseUri, string requestUri, object sendData)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = new TimeSpan(0, 0, 90);

                HttpResponseMessage resp = null;
                string sendJsonData = JsonConvert.SerializeObject(sendData);
                Inventec.Common.Logging.LogSystem.Info("_____sendJsonData : " + sendJsonData);

                string extension = baseUri.Substring(baseUri.IndexOf('/', baseUri.IndexOf("//") + 2));
                string fullrequestUri = requestUri;
                if (!requestUri.Contains(extension))
                {
                    fullrequestUri = extension + requestUri;
                }

                resp = client.PostAsync(fullrequestUri, new StringContent(sendJsonData, Encoding.UTF8, "application/json")).Result;

                if (resp == null || !resp.IsSuccessStatusCode)
                {
                    int statusCode = resp.StatusCode.GetHashCode();
                    throw new Exception(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}", baseUri, requestUri, statusCode));
                }

                string responseData = resp.Content.ReadAsStringAsync().Result;

                T data = JsonConvert.DeserializeObject<T>(responseData);
                if (data == null)
                {
                    throw new Exception(string.Format("Loi khi goi API: {0}{1}. Response {2}:", baseUri, requestUri, responseData));
                }
                return data;
            }
        }
    }
}
