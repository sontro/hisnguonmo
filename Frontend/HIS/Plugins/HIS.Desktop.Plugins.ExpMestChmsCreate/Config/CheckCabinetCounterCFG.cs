using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestChmsCreate.Config
{
    class CheckCabinetCounterCFG
    {
        private const string CONFIG_KEY = "HIS.DESKTOP.CHMS_EXP_MEST.CHECK_CABINET_COUNTER";
        private const string IS_CHECK = "1";

        private static bool? isCheckCabinetCounter;
        public static bool IsCheckCabinetCounter
        {
            get
            {
                if (!isCheckCabinetCounter.HasValue)
                {
                    isCheckCabinetCounter = GetBool(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY));
                }
                return isCheckCabinetCounter.Value;
            }
        }

        private static bool GetBool(string value)
        {
            try
            {
                return (value == IS_CHECK);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }
    }
}
