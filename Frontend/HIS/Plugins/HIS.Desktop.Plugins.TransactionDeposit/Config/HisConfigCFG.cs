using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDeposit.Config
{
    internal class HisConfigCFG
    {
        private const string His_Desktop_plugins_transactionTime_IsEditTransactionTime = "HIS.Desktop.Plugins.TransactionBill_Depo_Repa.IsEditTransactionTime";
        private const string HIS_Desktop_ShowServerTimeByDefault = "HIS.Desktop.ShowServerTimeByDefault";
        internal static string ShowServerTimeByDefault;
        internal static string IsEditTransactionTimeCFG;
        private const string CONFIG__CASHIER_ROOM_PAYMENT_OPTION = "MOS.EPAYMENT.CASHIER_ROOM_PAYMENT_OPTION";
        internal static string CashierRoomPaymentOption;
        internal static void LoadConfig()
        {
            try
            {
                LogSystem.Debug("LoadConfig => 1");
                IsEditTransactionTimeCFG = GetValue(His_Desktop_plugins_transactionTime_IsEditTransactionTime);
                CashierRoomPaymentOption = GetValue(CONFIG__CASHIER_ROOM_PAYMENT_OPTION);
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
        public enum OptionKey
        {
            Option1 = 1,
            Option2 = 2
        }
    }
}
