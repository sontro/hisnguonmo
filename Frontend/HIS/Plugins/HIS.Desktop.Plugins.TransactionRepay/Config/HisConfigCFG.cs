using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionRepay.Config
{
    internal class HisConfigCFG
    {

        private const string IsNotBillString = "HIS.Desktop.Plugins.TransactionRepay.IsNotBill";
        private const string HIS_Desktop_ShowServerTimeByDefault = "HIS.Desktop.ShowServerTimeByDefault";
        private const string His_Desktop_plugins_transactionTime_IsEditTransactionTime = "HIS.Desktop.Plugins.TransactionBill_Depo_Repa.IsEditTransactionTime";
        internal static string IsEditTransactionTimeCFG;
        internal static string ShowServerTimeByDefault;
      
        internal static string IsNotBillCFG;

        internal static void LoadConfig()
        {
            try
            {
                LogSystem.Debug("LoadConfig => 1");
                IsEditTransactionTimeCFG = GetValue(His_Desktop_plugins_transactionTime_IsEditTransactionTime);
                IsNotBillCFG = GetValue(IsNotBillString);
                ShowServerTimeByDefault = GetValue(HIS_Desktop_ShowServerTimeByDefault);
                LogSystem.Debug("LoadConfig => 2");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
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

        private static List<string> GetListValue(string key)
        {
            try
            {
                return HisConfigs.Get<List<string>>(key);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }
    }
}
