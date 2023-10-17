using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHealthExamRank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class HrmConfgiData
    {
        public string Address { get; set; }
        public string Loginname { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public bool CheckValid()
        {
            return !(String.IsNullOrWhiteSpace(Address)
                || String.IsNullOrWhiteSpace(Loginname)
                || String.IsNullOrWhiteSpace(Password)
                || String.IsNullOrWhiteSpace(GrantType)
                || String.IsNullOrWhiteSpace(ClientId)
                || String.IsNullOrWhiteSpace(ClientSecret));
        }
    }

    class HisHealthExamRankCFG
    {
        private const string CFG_HRM_CONNECT_INFO = "MOS.HR.ADDRESS";

        private static List<HIS_HEALTH_EXAM_RANK> data;
        public static List<HIS_HEALTH_EXAM_RANK> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisHealthExamRankGet().Get(new HisHealthExamRankFilterQuery());
                }
                return data;
            }
        }


        private static HrmConfgiData hrmConnectData;
        public static HrmConfgiData HRM_CONNECT_DATA
        {
            get
            {
                if (hrmConnectData == null)
                {
                    hrmConnectData = ParseHrmConfig(CFG_HRM_CONNECT_INFO);
                }
                return hrmConnectData;
            }
            set
            {
                hrmConnectData = value;
            }
        }


        private static HrmConfgiData ParseHrmConfig(string code)
        {
            try
            {
                HrmConfgiData result = new HrmConfgiData();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<HrmConfgiData>(data);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static void Reload()
        {
            var tmp = new HisHealthExamRankGet().Get(new HisHealthExamRankFilterQuery());
            var address = ParseHrmConfig(CFG_HRM_CONNECT_INFO);
            data = tmp;
            hrmConnectData = address;
        }
    }
}
