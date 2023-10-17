using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.AlertWarningFee
{
    class ConfigCFG
    {
        private const string CONFIG_KEY__IS_ALERT_HOSPITALFEE_NOTBHYT = "HIS.Desktop.Plugins.Library.AlertHospitalFeeNotBHYT";

        internal const string valueString__true = "1";
        internal const int valueInt__true = 1;

        internal static bool IsAlertHospitalFeeNotBHYT;

        internal static void LoadConfig()
        {
            IsAlertHospitalFeeNotBHYT = (GetValue(CONFIG_KEY__IS_ALERT_HOSPITALFEE_NOTBHYT) == valueString__true);
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
