using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.UTILITY
{
    public class RsaHash
    {
        public static string PublicKey
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCBdRGp9rFsCmx18c");
                sb.Append("Co8N+XSu/4vyFZ9FkobcB9WS7PiHhnBCotuN3rUMoj1iXJuZ1Tqkw7");
                sb.Append("AGjPJ1hBAa6cCMfSQlQX8FVDb+4136eJZp6M7AaNprd/KtnzLB3uIc");
                sb.Append("DAPsjhYKxMcWWY/ov7JqhRSePU7X7NZFCiMA6M+DGsQB3BPQIDAQAB");
                return sb.ToString();
            }
        }

        public static SdaLicenseSDO GetLicense(string license)
        {
            SdaLicenseSDO result = null;

            string decode = new Inventec.Common.HashUtil.RsaHashProcessor().Decrypt(license, PublicKey);
            decode = decode.Substring(decode.IndexOf('{'));
            if (!String.IsNullOrWhiteSpace(decode))
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<SdaLicenseSDO>(decode);
                result.License = license;
            }

            return result;
        }
    }
}
