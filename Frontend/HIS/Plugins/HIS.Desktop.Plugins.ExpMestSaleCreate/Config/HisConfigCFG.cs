using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreate.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__IS_JOIN_NAME_WITH_CONCENTRA = "HIS.HIS_MEDI_STOCK.IS_JOIN_NAME_WITH_CONCENTRA";//Noi ten thuoc vs ham luong
        private const string CONFIG_KEY__MANAGE_PATIENT_IN_SALE = "MOS.HIS_EXP_MEST.MANAGE_PATIENT_IN_SALE";

        private const string CONFIG_KEY__MUST_BE_FINISHED_BEFORED_PRINTING = "HIS.Desktop.Plugins.ExpMestSale.MustBeFinishedBeforePrinting";
        private const string CONFIG_KEY__IS_AUTO_SELECT_ACCOUNT_BOOK_IF_HAS_ONE = "HIS.Desktop.Plugins.TransactionBill.AutoSelectAccountBookIfHasOne";

        private const string CONFIG_KEY__IS_ROUND_PRICE_BASE = "HIS.Desktop.Plugins.Transaction.RoundPriceBase";

        internal static bool IS_JOIN_NAME_WITH_CONCENTRA;
        internal static bool IS_MUST_BE_FINISHED_BEFORED_PRINTING;
        internal static bool IS_MANAGE_PATIENT_IN_SALE;
        internal static bool IS_AUTO_SELECT_ACCOUNT_BOOK_IF_HAS_ONE;
        internal static string IS_ROUND_PRICE_BASE;

        internal static void LoadConfig()
        {
            try
            {
                IS_JOIN_NAME_WITH_CONCENTRA = GetValue(CONFIG_KEY__IS_JOIN_NAME_WITH_CONCENTRA) == "1";
                IS_MUST_BE_FINISHED_BEFORED_PRINTING = GetValue(CONFIG_KEY__MUST_BE_FINISHED_BEFORED_PRINTING) == "1";
                IS_MANAGE_PATIENT_IN_SALE = GetValue(CONFIG_KEY__MANAGE_PATIENT_IN_SALE) == "1";
                IS_AUTO_SELECT_ACCOUNT_BOOK_IF_HAS_ONE = GetValue(CONFIG_KEY__IS_AUTO_SELECT_ACCOUNT_BOOK_IF_HAS_ONE) == "1";
                IS_ROUND_PRICE_BASE = GetValue(CONFIG_KEY__IS_ROUND_PRICE_BASE);
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
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }

    }
}
