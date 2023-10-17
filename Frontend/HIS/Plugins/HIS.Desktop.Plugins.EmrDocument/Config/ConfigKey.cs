using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmrDocument.Config
{
    class ConfigKey
    {
        private const string CONFIG_KEY__EMR_PATIENT_SIGN_OPTION = "EMR.EMR_DOCUMENT.PATIENT_SIGN.OPTION";

        internal static string patientSignOption;

        internal static void GetConfigKey()
        {
            try
            {
                patientSignOption = GetValue(CONFIG_KEY__EMR_PATIENT_SIGN_OPTION);
                Inventec.Common.Logging.LogSystem.Debug(patientSignOption);
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
