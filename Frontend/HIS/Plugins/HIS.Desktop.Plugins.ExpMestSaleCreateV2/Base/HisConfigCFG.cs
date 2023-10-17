using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreateV2.Config
{
    internal class HisConfigCFG
    {

        private const string CONFIG_KEY__MUST_BE_FINISHED_BEFORED_PRINTING = "HIS.Desktop.Plugins.ExpMestSale.MustBeFinishedBeforePrinting";

        internal static bool IS_MUST_BE_FINISHED_BEFORED_PRINTING;

        internal static void LoadConfig()
        {
            try
            {
                IS_MUST_BE_FINISHED_BEFORED_PRINTING = GetValue(CONFIG_KEY__MUST_BE_FINISHED_BEFORED_PRINTING) == "1";
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static bool IsMustBeFinishBeforePrinting()
        {
            try
            {
                IS_MUST_BE_FINISHED_BEFORED_PRINTING = GetValue(CONFIG_KEY__MUST_BE_FINISHED_BEFORED_PRINTING) == "1";
                if (IS_MUST_BE_FINISHED_BEFORED_PRINTING)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static string GetValue(string code)
        {
            string result = null;
            try
            {
                return HisConfigs.Get<string>(code);
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
