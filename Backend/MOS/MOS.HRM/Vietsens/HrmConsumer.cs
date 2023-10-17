using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOS.HRM.Vietsens
{
    public class HrmConsumer
    {
        private string base_uri = null;
        private string grant_type = null;
        private string client_id = null;
        private string client_secret = null;
        private string username = null;
        private string password = null;

        private HrmTokenData token_data { get; set; }

        public HrmConsumer(string baseUri, string grantType, string clientId, string clientSecret, string user, string pass)
        {
            this.Init(baseUri, grantType, clientId, clientSecret, user, pass);
            if (!this.Login()) throw new Exception("Login Hrm faild");
        }

        private void Init(string baseUri, string grantType, string clientId, string clientSecret, string user, string pass)
        {
            this.base_uri = baseUri;
            this.grant_type = grantType;
            this.client_id = clientId;
            this.client_secret = clientSecret;
            this.username = user;
            this.password = pass;
        }

        private bool Login()
        {
            bool result = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.base_uri);
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", this.grant_type),
                        new KeyValuePair<string, string>("client_id", this.client_id) ,
                        new KeyValuePair<string, string>("client_secret", this.client_secret),
                        new KeyValuePair<string, string>("username", this.username),
                        new KeyValuePair<string, string>("password", this.password)
                    });

                    HttpResponseMessage resp = client.PostAsync("/WebService/oauth/token", formContent).Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        string respData = resp.Content.ReadAsStringAsync().Result;
                        this.token_data = Newtonsoft.Json.JsonConvert.DeserializeObject<HrmTokenData>(respData);
                        result = this.token_data != null;
                    }
                    else
                    {
                        LogSystem.Error(String.Format("Login faild: StatusCode = {0}, Content: {1}", resp.StatusCode, formContent.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool PostKsk(KskData data)
        {
            bool result = false;
            if (data != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.base_uri);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token_data.access_token);

                    //HttpResponseMessage resp = client.PostAsJsonAsync("WebService/api/v1/employee/insert-medical-health", data).Result;
                    string stringdata = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    //var formContent = new FormUrlEncodedContent(new[]
                    //{
                    //    new KeyValuePair<string, string>("data",stringdata)
                    //});

                    HttpResponseMessage resp = client.PostAsJsonAsync("WebService/api/v1/employee/medical/insert-medical-health", data).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        string jsonData = resp.Content.ReadAsStringAsync().Result;
                        KskResultData rs = Newtonsoft.Json.JsonConvert.DeserializeObject<KskResultData>(jsonData);
                        result = rs.statusType == "OK";
                        if (!result)
                        {
                            LogSystem.Error("Day thong tin KSK Nhan su. Response: " + jsonData);
                        }
                    }
                    else
                    {
                        LogSystem.Error("Goi aoi day thong tin KSK nhan su that bai: statusCode: " + resp.StatusCode);
                    }
                }
            }
            return result;
        }
    }
}
