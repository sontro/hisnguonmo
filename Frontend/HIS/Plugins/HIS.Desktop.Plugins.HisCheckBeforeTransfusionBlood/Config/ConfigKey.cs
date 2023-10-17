using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCheckBeforeTransfusionBlood.Config
{
    class ConfigKey
    {
        private const string Code_IsNotAllowEditBloodInformation = "HIS.Desktop.Plugins.BrowseExportTicket.IsNotAllowEditBloodInformation";

        internal static string IsNotAllowEditBloodInformation;

        internal static void GetConfigKey()
        {
            try
            {
                IsNotAllowEditBloodInformation = GetValue(Code_IsNotAllowEditBloodInformation);
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
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(code);
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
