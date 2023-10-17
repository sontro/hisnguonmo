using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignService.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__IsAutoStartServiceConnector = "EMR.Desktop.Plugins.IntegrateAssignPrescription.IsAutoStartServiceConnector";

        internal static bool IsAutoStartServiceConnector;
      
        internal static void LoadConfig()
        {
            try
            {
                IsAutoStartServiceConnector = GetValue(CONFIG_KEY__IsAutoStartServiceConnector) == GlobalVariables.CommonStringTrue;
              
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
