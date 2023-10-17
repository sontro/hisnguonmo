using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ManuExpMestCreate.Config
{
    internal class HisConfigCFG
    {
        private const string CONFIG_KEY__EXP_MEST__IS_REASON_REQUIRED = "MOS.EXP_MEST.IS_REASON_REQUIRED";

        internal static string ExpMestCFG_IsReasonRequired;

        internal static void LoadConfig()
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("LoadConfig => Start");

                ExpMestCFG_IsReasonRequired = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__EXP_MEST__IS_REASON_REQUIRED);

                Inventec.Common.Logging.LogSystem.Debug("LoadConfig => End");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
