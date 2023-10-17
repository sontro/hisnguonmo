using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaBloodCreate.ADO
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__IsTrackingRequired = "HIS.Desktop.Plugins.MobaCreate.IsTrackingRequired";

        internal static bool IsTrackingRequired;

        internal static void LoadConfig()
        {
            try
            {
                IsTrackingRequired = GetValue(CONFIG_KEY__IsTrackingRequired) == GlobalVariables.CommonStringTrue;
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
