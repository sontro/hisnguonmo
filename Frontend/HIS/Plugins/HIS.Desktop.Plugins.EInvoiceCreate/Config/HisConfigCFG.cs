using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EInvoiceCreate.Config
{
    class HisConfigCFG
    {
        private const string AutoSelectAccountBookIfHasOne = "HIS.Desktop.Plugins.TransactionBill.AutoSelectAccountBookIfHasOne";

        internal static bool IsAutoSelectAccountBookIfHasOne;

        internal static void LoadConfig()
        {
            try
            {
                LogSystem.Debug("LoadConfig => 1");
                IsAutoSelectAccountBookIfHasOne = HisConfigs.Get<string>(AutoSelectAccountBookIfHasOne) == "1";
                LogSystem.Debug("LoadConfig => 2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
