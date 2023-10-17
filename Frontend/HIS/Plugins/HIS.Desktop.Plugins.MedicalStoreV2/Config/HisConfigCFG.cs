using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStoreV2.Config
{
    class HisConfigCFG
    {

        private const string CONFIG_KEY__RED_WARNING_STORE_TIME = "HIS.Desktop.Plugins.MedicalStore.RED_WARNING_STORE_TIME";
        private const string CONFIG_KEY__BLUE_WARNING_STORE_TIME = "HIS.Desktop.Plugins.MedicalStore.BLUE_WARNING_STORE_TIME";
        private const string CONFIG_KEY__ORANGE_WARNING_STORE_TIME = "HIS.Desktop.Plugins.MedicalStore.ORANGE_WARNING_STORE_TIME";

        internal static long? RedWarningStoreTime;
        internal static long? BlueWarningStoreTime;
        internal static long? OrangeWarningStoreTime;

        internal static void LoadConfig()
        {
            try
            {
                string blue = GetValue(CONFIG_KEY__BLUE_WARNING_STORE_TIME);
                string red = GetValue(CONFIG_KEY__RED_WARNING_STORE_TIME);
                string orange = GetValue(CONFIG_KEY__ORANGE_WARNING_STORE_TIME);
                if (!String.IsNullOrWhiteSpace(blue))
                {
                    BlueWarningStoreTime = Convert.ToInt32(blue ?? "");
                }
                else
                {
                    BlueWarningStoreTime = null;
                }
                if (!String.IsNullOrWhiteSpace(red))
                {
                    RedWarningStoreTime = Convert.ToInt32(red ?? "");
                }
                else
                {
                    RedWarningStoreTime = null;
                }
                if (!String.IsNullOrWhiteSpace(orange))
                {
                    OrangeWarningStoreTime = Convert.ToInt32(orange ?? "");
                }
                else
                {
                    OrangeWarningStoreTime = null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static string GetValue(string code)
        {
            string result = null;
            try
            {
                return HisConfigs.Get<string>(code);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }
    }
}
