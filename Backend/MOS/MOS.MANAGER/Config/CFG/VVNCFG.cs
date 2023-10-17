using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class VVNCFG
    {
        private static string CONFIG_VVN_INFO_CONNECT = System.Configuration.ConfigurationManager.AppSettings["MOS.MANAGER.CONFIG.VVN.INFO_CONNECT"];

        private static VvnInfoConnect infoConnect;
        internal static VvnInfoConnect INFO_CONNECT
        {
            get
            {
                if (infoConnect == null)
                {
                    infoConnect = GetInfoConnect();
                }
                return infoConnect;
            }
            set
            {
                infoConnect = value;
            }
        }

        private static VvnInfoConnect GetInfoConnect()
        {
            VvnInfoConnect result = new VvnInfoConnect();
            try
            {
                if (String.IsNullOrWhiteSpace(CONFIG_VVN_INFO_CONNECT))
                {
                    throw new ArgumentNullException("CONFIG_VVN_INFO_CONNECT");
                }

                List<string> list = CONFIG_VVN_INFO_CONNECT.Split('|').ToList();
                if (list == null || list.Count < 3)
                {
                    throw new ArgumentNullException("Cau Hinh VVN CONNECT INFO thieu thong tin");
                }

                result.User = list[0];
                result.Key = list[1];
                result.RecognitionAddress = list[2];
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new VvnInfoConnect();
            }
            return result;
        }

        public static void Reload()
        {
            infoConnect = GetInfoConnect();
        }
    }

    class VvnInfoConnect
    {
        public string User { get; set; }
        public string Key { get; set; }
        public string RecognitionAddress { get; set; }
    }
}
