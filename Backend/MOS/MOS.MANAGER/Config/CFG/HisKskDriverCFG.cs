using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class KskDriverSyncInfo
    {
        public string BranchCode { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string CertificateLink { get; set; }
        public string CertificatePass { get; set; }
    }

    class HisKskDriverCFG
    {
        private const string CONNECTION_INFO_CFG = "MOS.HIS_KSK_DRIVER.CONNECTION_INFO";

        private static List<KskDriverSyncInfo> kskDriverSyncInfo;
        public static List<KskDriverSyncInfo> KSK_DRIVER_SYNC_INFO
        {
            get
            {
                if (kskDriverSyncInfo == null)
                {
                    kskDriverSyncInfo = GetByKey(CONNECTION_INFO_CFG);
                }
                return kskDriverSyncInfo;
            }
            set
            {
                kskDriverSyncInfo = value;
            }
        }

        private static List<KskDriverSyncInfo> GetByKey(string code)
        {
            List<KskDriverSyncInfo> result = new List<KskDriverSyncInfo>();
            try
            {
                new List<KskDriverSyncInfo>();
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    string[] branchGroup = data.Split('|');
                    foreach (var item in branchGroup)
                    {
                        string[] sp = item.Split(';');
                        if (sp.Length > 4)
                        {
                            KskDriverSyncInfo info = new KskDriverSyncInfo();
                            info.BranchCode = sp[0];
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
                result = new List<KskDriverSyncInfo>();
            }

            return result;
        }

        public static void Reload()
        {
            try
            {
                kskDriverSyncInfo = GetByKey(CONNECTION_INFO_CFG);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
