
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTransactionFilter : FilterBase
    {
        public List<long> CASHIER_ROOM_IDs { get; set; }
        public List<long> CASHOUT_IDs { get; set; }
        public List<long> BILL_IDs { get; set; }
        public List<long> DEBT_BILL_IDs { get; set; }

        public long? TRANSACTION_TYPE_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? ACCOUNT_BOOK_ID { get; set; }
        public long? PAY_FORM_ID { get; set; }
        public long? CANCEL_REASON_ID { get; set; }
        public long? CASHIER_ROOM_ID { get; set; }
        public long? CASHOUT_ID { get; set; }
        public long? TRANSACTION_TIME_FROM { get; set; }
        public long? TRANSACTION_TIME_TO { get; set; }
        public long? NUM_ORDER_FROM { get; set; }
        public long? NUM_ORDER_TO { get; set; }
        public bool? HAS_CASHOUT { get; set; }
        public long? BILL_ID { get; set; }
        public long? NUM_ORDER__EQUAL { get; set; }
        public long? DEBT_BILL_ID { get; set; }
        public long? DEBT_TYPE { get; set; }
        public long? ORIGINAL_TRANSACTION_ID { get; set; }

        public bool? HAS_TDL_SERE_SERV_DEPOSIT { get; set; }
        public bool? HAS_TDL_SESE_DEPO_REPAY { get; set; }
        public bool? HAS_NATIONAL_TRANSACTION_CODE { get; set; }

        public string TRANSACTION_CODE__EXACT { get; set; }
        public string NATIONAL_TRANSACTION_CODE__EXACT { get; set; }
        public string TDL_TREATMENT_CODE__EXACT { get; set; }
        public string INVOICE_CODE__EXACT { get; set; }

        public bool? BILL_TYPE_IS_NULL_OR_EQUAL_1 { get; set; }
        public long? SALE_TYPE_ID { get; set; }
        public bool? HAS_SALE_TYPE_ID { get; set; }
        public bool? IS_DEBT_COLLECTION { get; set; }
        public bool? HAS_DEBT_BILL_ID { get; set; }
        public bool? IS_CANCEL { get; set; }
        public bool? HAS_INVOICE_CODE { get; set; }
        public short? IS_DELETE { get; set; }
        public HisTransactionFilter()
            : base()
        {
        }
    }
}
