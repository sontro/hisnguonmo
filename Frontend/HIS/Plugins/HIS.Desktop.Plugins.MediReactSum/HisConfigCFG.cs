using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediReactSum
{
    internal class HisConfigCFG
    {
        private const string CONFIG_KEY__ICD_GENERATE = "HIS.Desktop.Plugins.AutoCheckIcd";
        internal static string AutoCheckIcd;

        internal static void LoadConfig()
        {
            try
            {
                AutoCheckIcd = GetValue(CONFIG_KEY__ICD_GENERATE);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static string GetValue(string key)
        {
            try
            {
                return HisConfigs.Get<string>(key);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return "";
        }
    }
}
