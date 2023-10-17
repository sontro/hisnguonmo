using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqResultView
{
    public class PacsAddress
    {
        public string RoomCode { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Api { get; set; }
        public string CloudInfo { get; set; }
    }

    class PacsCFG
    {
        private const string PACS_ADDRESS_CFG = "MOS.PACS.ADDRESS";

        internal static List<PacsAddress> PACS_ADDRESS
        {
            get
            {
                return GetAddress(PACS_ADDRESS_CFG); ;
            }
        }

        private static List<PacsAddress> GetAddress(string code)
        {
            List<PacsAddress> result = new List<PacsAddress>();
            try
            {
                string value = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(code);
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(code);
                }
                List<PacsAddress> adds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PacsAddress>>(value);
                if (adds == null || adds.Count == 0)
                {
                    throw new AggregateException(code);
                }
                result.AddRange(adds);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new List<PacsAddress>();
            }
            return result;
        }
    }
}
