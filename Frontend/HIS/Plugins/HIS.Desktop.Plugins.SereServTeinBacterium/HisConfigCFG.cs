using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SereServTeinBacterium.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__AUTO_RETURN_RESULT_BEFORE_PRINT = "HIS.Desktop.Plugins.ReturnMicrobiologicalResults__AutoReturnResultBeforePrint";
        private const string CONFIG_KEY__RETURN_RESULT_WARNING_TIME = "HIS.Desktop.Plugins.ReturnMicrobiologicalResults__ReturnResultWarningTime";

        private const string CONFIG_KEY__IS_USE_SIGN_EMR = "HIS.HIS.DESKTOP.IS_USE_SIGN_EMR";

        internal static string IS_USE_SIGN_EMR;
        internal static string AUTO_RETURN_RESULT_BEFORE_PRINT;
        internal static string WARNING_TIME_RETURN_RESULT;

        internal static void LoadConfig()
        {
            try
            {
                IS_USE_SIGN_EMR = GetValue(CONFIG_KEY__IS_USE_SIGN_EMR);
                AUTO_RETURN_RESULT_BEFORE_PRINT = GetValue(CONFIG_KEY__AUTO_RETURN_RESULT_BEFORE_PRINT);
                WARNING_TIME_RETURN_RESULT = GetValue(CONFIG_KEY__RETURN_RESULT_WARNING_TIME);
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
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
