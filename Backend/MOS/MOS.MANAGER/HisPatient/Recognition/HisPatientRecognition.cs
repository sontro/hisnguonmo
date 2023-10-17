using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient.Recognition
{
    class HisPatientRecognition : BusinessBase
    {
        internal HisPatientRecognition()
            : base()
        {

        }
        internal HisPatientRecognition(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(RecognitionSDO data, ref RecognitionResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                valid = valid && !String.IsNullOrWhiteSpace(data.ImageBase64);
                if (valid)
                {
                    if (VVNCFG.INFO_CONNECT == null
                        || String.IsNullOrWhiteSpace(VVNCFG.INFO_CONNECT.RecognitionAddress)
                        || String.IsNullOrWhiteSpace(VVNCFG.INFO_CONNECT.User)
                        || String.IsNullOrWhiteSpace(VVNCFG.INFO_CONNECT.Key))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Vvn_ThieuThongTinCauHinh);
                        return false;
                    }

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("user", VVNCFG.INFO_CONNECT.User);
                        client.DefaultRequestHeaders.Add("key", VVNCFG.INFO_CONNECT.Key);
                        var options = new
                        {
                            image = data.ImageBase64
                        };

                        // Serialize our concrete class into a JSON String
                        var stringPayload = JsonConvert.SerializeObject(options);
                        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = client.PostAsync(VVNCFG.INFO_CONNECT.RecognitionAddress, content).Result;
                        string rs = null;
                        try
                        {
                            rs = response.Content.ReadAsStringAsync().Result;
                            resultData = JsonConvert.DeserializeObject<RecognitionResultSDO>(rs);
                        }
                        catch (Exception ex)
                        {
                            Inventec.Common.Logging.LogSystem.Error(LogUtil.TraceData("rs", rs), ex);
                        }
                    }
                    if (resultData != null)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
