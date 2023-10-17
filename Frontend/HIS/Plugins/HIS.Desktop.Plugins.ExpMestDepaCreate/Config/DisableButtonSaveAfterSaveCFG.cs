using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestDepaCreate.Config
{
    class DisableButtonSaveAfterSaveCFG
    {
        private const string CONFIG_KEY = "HIS.DESKTOP.DEPA_EXP_MEST_CREATE.DISABLE_BUTTON_SAVE_AFTER_SAVE";
        private const string IS_DISABLE = "1";

        private static bool? isDisable;
        public static bool IsDisable
        {
            get
            {
                if (!isDisable.HasValue)
                {
                    isDisable = GetBool(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY));
                }
                return isDisable.Value;
            }
        }

        private static bool GetBool(string value)
        {
            try
            {
                return (value == IS_DISABLE);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }
    }
}
