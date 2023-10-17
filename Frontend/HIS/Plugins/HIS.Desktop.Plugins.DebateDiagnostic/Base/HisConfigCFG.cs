using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DebateDiagnostic.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__IS_USE_SIGN_EMR = "HIS.HIS.DESKTOP.IS_USE_SIGN_EMR";
        private const string CONFIG_KEY__DebateDiagnostic_IsDefaultTracking = "HIS.Desktop.Plugins.DebateDiagnostic.IsDefaultTracking";

        internal static bool IsUseSignEmr;
        internal static bool DebateDiagnostic_IsDefaultTracking;
        internal static void LoadConfig()
        {
            try
            {
                IsUseSignEmr = GetValue(CONFIG_KEY__IS_USE_SIGN_EMR) == "1";
                DebateDiagnostic_IsDefaultTracking = GetValue(CONFIG_KEY__DebateDiagnostic_IsDefaultTracking) == "1";
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
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }

    }
}
