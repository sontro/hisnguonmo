using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaCabinetCreate
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__AUTO_SELECT_IMP_MEDI_STOCK = "MOS.HIS_IMP_MEST.MOBA_PRES_CABINET.AUTO_SELECT_IMP_MEDI_STOCK";
        private const string CONFIG_KEY__MOBA_INTO_MEDI_STOCK_EXPORT = "MOS.HIS_MOBA_IMP_MEST.CABINET.MOBA_INTO_MEDI_STOCK_EXPORT";
        private const string CONFIG_KEY__IsTrackingRequired = "HIS.Desktop.Plugins.MobaCreate.IsTrackingRequired";

        internal static bool IsAutoSelectImpMediStock;
        internal static bool IsMobaIntoMediStockExport;
        internal static bool IsTrackingRequired;

        internal static void LoadConfig()
        {
            try
            {
                IsAutoSelectImpMediStock = GetValue(CONFIG_KEY__AUTO_SELECT_IMP_MEDI_STOCK) == GlobalVariables.CommonStringTrue;
                IsMobaIntoMediStockExport = GetValue(CONFIG_KEY__MOBA_INTO_MEDI_STOCK_EXPORT) == GlobalVariables.CommonStringTrue;
                IsTrackingRequired = GetValue(CONFIG_KEY__IsTrackingRequired) == GlobalVariables.CommonStringTrue;
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
                return HisConfigs.Get<string>(code);
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }
        
    }
}
