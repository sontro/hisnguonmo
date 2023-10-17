using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UserInfo.Config
{
   internal class ConfigCFG
    {
        private const string CONFIG_KEY__InterconnectionPrescription = "HIS.Desktop.Plugins.InterconnectionPrescription.SysConfig";//Doi tuong BHYT
        internal static string InterconnectionPrescription;

        internal static void LoadConfig()
        {
            try
            {
                InterconnectionPrescription = HisConfigs.Get<string>(CONFIG_KEY__InterconnectionPrescription);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
