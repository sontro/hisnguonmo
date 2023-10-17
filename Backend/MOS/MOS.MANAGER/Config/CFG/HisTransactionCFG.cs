using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisTransactionCFG
    {
        private const string SET_TREATMENT_FEE_INFO_CFG = "MOS.HIS_TRANSACTION.SET_TREATMENT_FEE_INFO";

        private const string IS_USING_SERVER_TIME_CFG = "MOS.HIS_TRANSACTION.IS_USING_SERVER_TIME";

        private const string CFG_TRANSACTION_BILL_TWO_BOOK__OPTION = "MOS.HIS_TRANSACTION.BILL_TWO_BOOK.OPTION";

        private const string CFG_MUST_FINISH_TREATMENT = "MOS.HIS_BILL.BHYT.MUST_FINISH_TREATMENT_BEFORE_BILLING";
        private const string CFG_IS_ALLOW_EDIT_NUM_ORDER = "MOS.HIS_TRANSACTION.NUM_ORDER.ALLOW_EDIT";
        private const string CFG_ALLOW_UPDATE_ACCOUNT_BOOK = "MOS.HIS_TRANSACTION.ALLOW_UPDATE_ACCOUNT_BOOK";
        private const string CFG_VERIFY_INVOICE_INFO_MATERIAL_BEFORE_BILLING = "MOS.HIS_TRANSACTION.VERIFY_INVOICE_INFO_MATERIAL_BEFORE_BILLING";
        private const string CFG_DO_NOT_ALLOW_CANCEL_BILL_IF_SERVICE_REQ_IS_PROCESSED = "MOS.HIS_TRANSACTION.DO_NOT_ALLOW_CANCEL_BILL_IF_SERVICE_REQ_IS_PROCESSED";
        private const string CFG_ALLOW_PROCESS_WITH_NO_TRANSACTION = "HIS.Desktop.Plugins.TransactionBill.EnableSaveOption";
        private const string CFG_ALLOW_TO_CREATE_NO_PRICE_TRANSACTION = "HIS.Desktop.Plugins.TransactionBill.AllowToCreateNoPriceTransaction";
        private const string CFG_ALLOW_TO_DEPOSIT_INPATIENT_PRESCRIPTION = "MOS.HIS_TRANSACTION.ALLOW_TO_DEPOSIT_INPATIENT_PRESCRIPTION";

        internal enum ENUM_BILL_OPTION
        {
            CTO_TW = 1,
            HCM_115 = 2,
            QBH_CUBA = 3,
        }

        internal enum MUST_FINISH_TREATMENT
        {
            BHYT_ONLY = 1,
            ALL = 2,
        }

        private static bool? isUsingServerTime;
        public static bool IS_USING_SERVER_TIME
        {
            get
            {
                if (!isUsingServerTime.HasValue)
                {
                    isUsingServerTime = ConfigUtil.GetIntConfig(IS_USING_SERVER_TIME_CFG) == 1;
                }
                return isUsingServerTime.Value;
            }
        }

        private static bool? setTreatmentFeeInfo;
        public static bool SET_TREATMENT_FEE_INFO
        {
            get
            {
                if (!setTreatmentFeeInfo.HasValue)
                {
                    setTreatmentFeeInfo = ConfigUtil.GetIntConfig(SET_TREATMENT_FEE_INFO_CFG) == 1;
                }
                return setTreatmentFeeInfo.Value;
            }
            set
            {
                setTreatmentFeeInfo = value;
            }
        }

        private static int? billTwoBookOption;
        public static int BILL_TWO_BOOK_OPTION
        {
            get
            {
                if (!billTwoBookOption.HasValue)
                {
                    billTwoBookOption = ConfigUtil.GetIntConfig(CFG_TRANSACTION_BILL_TWO_BOOK__OPTION);
                }
                return billTwoBookOption.Value;
            }
            set
            {
                billTwoBookOption = value;
            }
        }

        private static int? mustFinishTreatmentForBill;
        public static int MUST_FINISH_TREATMENT_FOR_BILL
        {
            get
            {
                if (!mustFinishTreatmentForBill.HasValue)
                {
                    mustFinishTreatmentForBill = ConfigUtil.GetIntConfig(CFG_MUST_FINISH_TREATMENT);
                }
                return mustFinishTreatmentForBill.Value;
            }
            set
            {
                mustFinishTreatmentForBill = value;
            }
        }

        private static bool? isAllowEditNumOrder;
        public static bool IS_ALLOW_EDIT_NUM_ORDER
        {
            get
            {
                if (!isAllowEditNumOrder.HasValue)
                {
                    isAllowEditNumOrder = ConfigUtil.GetIntConfig(CFG_IS_ALLOW_EDIT_NUM_ORDER) == 1;
                }
                return isAllowEditNumOrder.Value;
            }
            set
            {
                isAllowEditNumOrder = value;
            }
        }

        private static bool? allowUpdateAccountBook;
        public static bool ALLOW_UPDATE_ACCOUNT_BOOK
        {
            get
            {
                if (!allowUpdateAccountBook.HasValue)
                {
                    allowUpdateAccountBook = ConfigUtil.GetIntConfig(CFG_ALLOW_UPDATE_ACCOUNT_BOOK) == 1;
                }
                return allowUpdateAccountBook.Value;
            }
            set
            {
                allowUpdateAccountBook = value;
            }
        }

        private static bool? verifyInvoiceInfoMaterialBeforeBilling;
        public static bool VERIFY_INVOICE_MATERIAL_BEFORE_BILLING
        {
            get
            {
                if (!verifyInvoiceInfoMaterialBeforeBilling.HasValue)
                {
                    verifyInvoiceInfoMaterialBeforeBilling = ConfigUtil.GetIntConfig(CFG_VERIFY_INVOICE_INFO_MATERIAL_BEFORE_BILLING) == 1;
                }
                return verifyInvoiceInfoMaterialBeforeBilling.Value;
            }
            set
            {
                verifyInvoiceInfoMaterialBeforeBilling = value;
            }
        }

        private static bool? doNotAllowCancelBillIfServiceReqIsProcessed;
        public static bool DO_NOT_ALLOW_CANCEL_BILL_IF_SERVICE_REQ_IS_PROCESSED
        {
            get
            {
                if (!doNotAllowCancelBillIfServiceReqIsProcessed.HasValue)
                {
                    doNotAllowCancelBillIfServiceReqIsProcessed = ConfigUtil.GetIntConfig(CFG_DO_NOT_ALLOW_CANCEL_BILL_IF_SERVICE_REQ_IS_PROCESSED) == 1;
                }
                return doNotAllowCancelBillIfServiceReqIsProcessed.Value;
            }
            set
            {
                doNotAllowCancelBillIfServiceReqIsProcessed = value;
            }
        }

        private static bool? isAllowProcessWithNoTransaction;
        public static bool IS_ALLOW_PROCESS_WITH_NO_TRANSACTION
        {
            get
            {
                if (!isAllowProcessWithNoTransaction.HasValue)
                {
                    isAllowProcessWithNoTransaction = ConfigUtil.GetIntConfig(CFG_ALLOW_PROCESS_WITH_NO_TRANSACTION) == 1;
                }
                return isAllowProcessWithNoTransaction.Value;
            }
            set
            {
                isAllowProcessWithNoTransaction = value;
            }
        }

        private static bool? isAllowToCreateNoPriceTransaction;
        public static bool IS_ALLOW_TO_CREATE_NO_PRICE_TRANSACTION
        {
            get
            {
                if (!isAllowToCreateNoPriceTransaction.HasValue)
                {
                    isAllowToCreateNoPriceTransaction = ConfigUtil.GetIntConfig(CFG_ALLOW_TO_CREATE_NO_PRICE_TRANSACTION) == 1;
                }
                return isAllowToCreateNoPriceTransaction.Value;
            }
            set
            {
                isAllowToCreateNoPriceTransaction = value;
            }
        }

        private static bool? isAllowToDepositInPatientPrescription;
        public static bool IS_ALLOW_TO_DEPOSIT_INPATIENT_PRESCRIPTION
        {
            get
            {
                if (!isAllowToDepositInPatientPrescription.HasValue)
                {
                    isAllowToDepositInPatientPrescription = ConfigUtil.GetIntConfig(CFG_ALLOW_TO_DEPOSIT_INPATIENT_PRESCRIPTION) == 1;
                }
                return isAllowToDepositInPatientPrescription.Value;
            }
            set
            {
                isAllowToDepositInPatientPrescription = value;
            }
        }
        //private static List<V_HIS_TRANSACTION> data;
        //public static List<V_HIS_TRANSACTION> DATAVIEW
        //{
        //    get
        //    {
        //        if (data == null)
        //        {
        //            data = new HisTransactionGet().GetView(new HisTransactionViewFilterQuery());
        //        }
        //        return data;
        //    }
        //}

        public static void Reload()
        {
            setTreatmentFeeInfo = ConfigUtil.GetIntConfig(SET_TREATMENT_FEE_INFO_CFG) == 1;
            isUsingServerTime = ConfigUtil.GetIntConfig(IS_USING_SERVER_TIME_CFG) == 1;
            billTwoBookOption = ConfigUtil.GetIntConfig(CFG_TRANSACTION_BILL_TWO_BOOK__OPTION);
            mustFinishTreatmentForBill = ConfigUtil.GetIntConfig(CFG_MUST_FINISH_TREATMENT);
            isAllowEditNumOrder = ConfigUtil.GetIntConfig(CFG_IS_ALLOW_EDIT_NUM_ORDER) == 1;
            allowUpdateAccountBook = ConfigUtil.GetIntConfig(CFG_ALLOW_UPDATE_ACCOUNT_BOOK) == 1;
            verifyInvoiceInfoMaterialBeforeBilling = ConfigUtil.GetIntConfig(CFG_VERIFY_INVOICE_INFO_MATERIAL_BEFORE_BILLING) == 1;
            doNotAllowCancelBillIfServiceReqIsProcessed = ConfigUtil.GetIntConfig(CFG_DO_NOT_ALLOW_CANCEL_BILL_IF_SERVICE_REQ_IS_PROCESSED) == 1;
            isAllowProcessWithNoTransaction = ConfigUtil.GetIntConfig(CFG_ALLOW_PROCESS_WITH_NO_TRANSACTION) == 1;
            isAllowToCreateNoPriceTransaction = ConfigUtil.GetIntConfig(CFG_ALLOW_TO_CREATE_NO_PRICE_TRANSACTION) == 1;
            isAllowToDepositInPatientPrescription = ConfigUtil.GetIntConfig(CFG_ALLOW_TO_DEPOSIT_INPATIENT_PRESCRIPTION) == 1;

        }
    }
}
