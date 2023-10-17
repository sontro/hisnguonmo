using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class BabySyncInfo
    {
        public string HeinMediOrgCode { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string CertificateLink { get; set; }
        public string CertificatePass { get; set; }
    }

    class HisBabyCFG
    {
        private const string CONNECTION_INFO_CFG = "MOS.HIS_BABY.CONNECTION_INFO";

        private static List<BabySyncInfo> babySyncInfo;
        public static List<BabySyncInfo> BABY_SYNC_INFO
        {
            get
            {
                if (babySyncInfo == null)
                {
                    babySyncInfo = GetByKey(CONNECTION_INFO_CFG);
                }
                return babySyncInfo;
            }
            set
            {
                babySyncInfo = value;
            }
        }

        private static List<BabySyncInfo> GetByKey(string code)
        {
            List<BabySyncInfo> result = new List<BabySyncInfo>();
            try
            {
                new List<BabySyncInfo>();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    string[] branchGroup = data.Split('|');
                    foreach (var item in branchGroup)
                    {
                        string[] sp = item.Split(';');
                        if (sp.Length > 4)
                        {
                            BabySyncInfo info = new BabySyncInfo();
                            info.HeinMediOrgCode = sp[0];
                            info.User = sp[1];
                            info.Password = sp[2];
                            info.Url = sp[3];
                            if (sp.Length > 4)
                            {
                                info.CertificateLink = sp[4];
                            }
                            if (sp.Length > 5)
                            {
                                info.CertificatePass = sp[5];
                            }

                            result.Add(info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = new List<BabySyncInfo>();
            }

            return result;
        }

        public static void Reload()
        {
            try
            {
                babySyncInfo = GetByKey(CONNECTION_INFO_CFG);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
