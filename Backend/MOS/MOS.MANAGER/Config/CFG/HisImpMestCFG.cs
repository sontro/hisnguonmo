using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisImpMestCFG
    {
        private const string MOBA_PRES__IS_APPROVE_BEFORE_TREATMENT_FINISHED = "MOS.HIS_IMP_MEST.MOBA_PRES.MUST_APPROVE_BEFORE_TREATMENT_FINISHED";
        private const string IS_AUTO_RECREATE_INVOICE_AFTER_WITHDRAWING = "MOS.HIS_IMP_MEST.IS_AUTO_RECREATE_INVOICE_AFTER_WITHDRAWING";
        private const string CONFIG_KEY__AGGR_MOBA_PRES__ODD_MANAGER_OPTION = "MOS.HIS_IMP_MEST.AGGR_MOBA_PRES.ODD_MANAGER_OPTION";
        private const string CONFIG_KEY__MAX_SUSPENDING_DAY_ALLOWED_FOR_PRES = "MOS.HIS_IMP_MEST.MAX_SUSPENDING_DAY_ALLOWED_FOR_PRESCRIPTION";
        private const string CONFIG_KEY__MOPA_PRES_CABINET_AUTO_SELECT_IMP_MEDI_STOCK = "MOS.HIS_IMP_MEST.MOBA_PRES_CABINET.AUTO_SELECT_IMP_MEDI_STOCK";

        public const string ControlCode_CancelImport = "HIS000042";

        internal enum OddManagerOption
        {
            ADDICTIVE_PSYCHOACITVE = 1,
            DEFAULT = 0,
        }

        private static bool? mustApproveBeforeTreatmentFinished;
        public static bool MUST_APPROVE_BEFORE_TREATMENT_FINISHED
        {
            get
            {
                if (!mustApproveBeforeTreatmentFinished.HasValue)
                {
                    mustApproveBeforeTreatmentFinished = ConfigUtil.GetStrConfig(MOBA_PRES__IS_APPROVE_BEFORE_TREATMENT_FINISHED) == "1";
                }
                return mustApproveBeforeTreatmentFinished.Value;
            }
        }

        private static bool? isAutoCreateTransactionWhenMobaExpMestSale;
        public static bool IS_AUTO_CREATE_TRAN_WHEN_MOBA_EXP_MEST_SALE
        {
            get
            {
                if (!isAutoCreateTransactionWhenMobaExpMestSale.HasValue)
                {
                    isAutoCreateTransactionWhenMobaExpMestSale = ConfigUtil.GetStrConfig(IS_AUTO_RECREATE_INVOICE_AFTER_WITHDRAWING) == "1";
                }
                return isAutoCreateTransactionWhenMobaExpMestSale.Value;
            }
        }

        private static int? oddManagementOption;
        public static int ODD_MANAGEMENT_OPTION
        {
            get
            {
                if (!oddManagementOption.HasValue)
                {
                    oddManagementOption = ConfigUtil.GetIntConfig(CONFIG_KEY__AGGR_MOBA_PRES__ODD_MANAGER_OPTION);
                }
                return oddManagementOption.Value;
            }
        }

        private static int? maxSuspendingDayAllowedForPrescription;
        public static int MAX_SUSPENDING_DAY_ALLOWED_FOR_PRESCRIPTION
        {
            get
            {
                if (!maxSuspendingDayAllowedForPrescription.HasValue)
                {
                    maxSuspendingDayAllowedForPrescription = ConfigUtil.GetIntConfig(CONFIG_KEY__MAX_SUSPENDING_DAY_ALLOWED_FOR_PRES);
                }
                return maxSuspendingDayAllowedForPrescription.Value;
            }
        }

        private static bool? autoSetImpMediStockMobaPresCabinet;
        public static bool AUTO_SET_IMP_STOCK_MOBA_PRES_CABINET
        {
            get
            {
                if (!autoSetImpMediStockMobaPresCabinet.HasValue)
                {
                    autoSetImpMediStockMobaPresCabinet = ConfigUtil.GetStrConfig(CONFIG_KEY__MOPA_PRES_CABINET_AUTO_SELECT_IMP_MEDI_STOCK) == "1";
                }
                return autoSetImpMediStockMobaPresCabinet.Value;
            }
        }

        public static void Reload()
        {
            mustApproveBeforeTreatmentFinished = ConfigUtil.GetStrConfig(MOBA_PRES__IS_APPROVE_BEFORE_TREATMENT_FINISHED) == "1";
            isAutoCreateTransactionWhenMobaExpMestSale = ConfigUtil.GetStrConfig(IS_AUTO_RECREATE_INVOICE_AFTER_WITHDRAWING) == "1";
            oddManagementOption = ConfigUtil.GetIntConfig(CONFIG_KEY__AGGR_MOBA_PRES__ODD_MANAGER_OPTION);
            maxSuspendingDayAllowedForPrescription = ConfigUtil.GetIntConfig(CONFIG_KEY__MAX_SUSPENDING_DAY_ALLOWED_FOR_PRES);
            autoSetImpMediStockMobaPresCabinet = ConfigUtil.GetStrConfig(CONFIG_KEY__MOPA_PRES_CABINET_AUTO_SELECT_IMP_MEDI_STOCK) == "1";
        }
    }
}
