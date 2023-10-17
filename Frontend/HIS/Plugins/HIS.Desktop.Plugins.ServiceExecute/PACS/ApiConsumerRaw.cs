using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HIS.Desktop.Plugins.ServiceExecute.PACS
{
    class ApiConsumerRaw
    {
        public ApiConsumerRaw() { }
        const int TIME_OUT = 30;
        internal T PostRaw<T>(string uri, object data, int userTimeout, params object[] listParam)
        {
            T result = default(T);
            using (var client = new HttpClient())
            {
                string requestedUrl = "";
                this.HttpRequestBuilder(client, uri, ref requestedUrl, userTimeout, listParam);
                HttpResponseMessage resp = client.PostAsJsonAsync(requestedUrl, data).Result;
                if (!resp.IsSuccessStatusCode)
                {
                    Inventec.Common.Logging.LogSystem.Error(string.Format("Loi khi goi API: {0}{1}. StatusCode: {2}. Input: {3}.", client.BaseAddress.AbsoluteUri, uri, resp.StatusCode.GetHashCode(), JsonConvert.SerializeObject(data)));
                }
                string responseData = resp.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<T>(responseData);
            }
            return result;
        }

        /// <summary>
        /// Tao HttpRequest voi cac tham so cho truoc
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri"></param>
        /// <param name="requestedUrl"></param>
        /// <param name="listParam"></param>
        private void HttpRequestBuilder(HttpClient client, string uri, ref string requestedUrl, int userTimeout, params object[] listParam)
        {
            client.DefaultRequestHeaders.Add(HeaderConstants.APPLICATION_CODE_PARAM, HIS.Desktop.LocalStorage.LocalData.GlobalVariables.APPLICATION_CODE);
            client.BaseAddress = new Uri(HIS.Desktop.LocalStorage.ConfigSystem.ConfigSystems.URI_API_PACS);
            client.DefaultRequestHeaders.Accept.Clear();
            client.Timeout = new TimeSpan(0, 0, (userTimeout > 0 ? userTimeout : TIME_OUT));

            requestedUrl = string.Format("{0}?", uri);
            if (listParam != null && listParam.Length > 0)
            {
                if (listParam.Length % 2 != 0)
                {
                    throw new ArgumentException("Danh sach param khong hop le. So luong param phai la so chan");
                }
                for (int i = 0; i < listParam.Length; )
                {
                    requestedUrl += string.Format("{0}={1}&", HttpUtility.UrlEncode(listParam[i] + ""), HttpUtility.UrlEncode(listParam[i + 1] + ""));
                    i = i + 2;
                }
            }
        }

        public class HeaderConstants
        {
            public const string ADDRESS_PARAM = "Address";
            public const string APPLICATION_CODE_PARAM = "ApplicationCode";
            public const string BASIC_AUTH_HEADER = "Authorization";
            public const string BASIC_AUTH_HEADER_RESPONSE = "WWW-Authenticate";
            public const string BASIC_AUTH_SCHEME = "Basic";
            public const string DATA_KEY_PARAM = "DataKey";
            public const string PASS_PARAM = "Password";
        }
    }
}
