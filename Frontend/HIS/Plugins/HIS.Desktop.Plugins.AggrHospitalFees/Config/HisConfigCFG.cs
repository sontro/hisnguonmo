using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrHospitalFees.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__IsShowRepayPriceCFG = "HIS.Desktop.Plugins.Transaction.IsSplitTotalReceivePrice";
        internal static string IsShowRepayPriceCFG;
  
        internal static void LoadConfig()
        {
            try
            {
                IsShowRepayPriceCFG = GetValue(CONFIG_KEY__IsShowRepayPriceCFG);
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
