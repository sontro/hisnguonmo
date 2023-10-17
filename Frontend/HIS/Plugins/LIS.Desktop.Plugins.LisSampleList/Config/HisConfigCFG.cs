using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisSampleList.Config
{
    class HisConfigCFG
    {
        private const string PlatformOptionCFG = "Inventec.Common.DocumentViewer.PlatformOption";
        private const string SyncResultCFG = "LIS.Desktop.Plugins.LisSampleList.ConnectionInfo";
        internal static int PlatformOption;
        internal static string SyncResult;
        internal static void LoadConfig()
        {
            try
            {
                LogSystem.Debug("LoadConfig => 1");
                PlatformOption = HisConfigs.Get<int>(PlatformOptionCFG);
                SyncResult = HisConfigs.Get<string>(SyncResultCFG);
                LogSystem.Debug("LoadConfig => 2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
