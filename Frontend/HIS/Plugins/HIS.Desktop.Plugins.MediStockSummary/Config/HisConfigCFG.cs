using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockSummary.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__IS_USE_BASE_AMOUNT_CABINET = "MOS.HIS_MEDI_STOCK.CABINET.IS_USE_BASE_AMOUNT";

        internal static string IS_USE_BASE_AMOUNT;

        internal static void LoadConfig()
        {
            try
            {
                IS_USE_BASE_AMOUNT = HisConfigs.Get<string>(CONFIG_KEY__IS_USE_BASE_AMOUNT_CABINET);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
