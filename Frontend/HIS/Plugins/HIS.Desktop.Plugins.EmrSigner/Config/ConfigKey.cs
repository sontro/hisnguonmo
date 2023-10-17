using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmrSigner.Config
{
    class ConfigKey
    {
        private const string CONFIG_KEY__HSM_INTEGRATE_OPTION = "EMR.HSM.INTEGRATE_OPTION";

        internal static string intergrateOption;

        internal static void GetConfigKey()
        {
            try
            {
                intergrateOption = GetValue(CONFIG_KEY__HSM_INTEGRATE_OPTION);
                Inventec.Common.Logging.LogSystem.Debug(intergrateOption);
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
                return HIS.Desktop.LocalStorage.EmrConfig.EmrConfigs.Get<string>(code);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }
    }
}
