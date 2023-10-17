using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    public class EncryptUtil
    {
        public static string EncodeData(object data)
        {
            string result = "";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            result = System.Convert.ToBase64String(plainTextBytes);
            return result;
        }

        public static string DecodeData(string data)
        {
            string result = "";
            var base64EncodedBytes = System.Convert.FromBase64String(data);
            result = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            return result;
        }
    }
}
