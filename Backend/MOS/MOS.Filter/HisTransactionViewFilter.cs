using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisTransactionViewFilter : FilterBase
    {
        public List<long> TRANSACTION_TYPE_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> ACCOUNT_BOOK_IDs { get; set; }
        public List<long> CASHOUT_IDs { get; set; }
        public List<long> DEBT_BILL_IDs { get; set; }
        
        public long? TRANSACTION_TIME_FROM { get; set; }
        public long? TRANSACTION_TIME_TO { get; set; }
        public long? NUM_ORDER_FROM { get; set; }
        public long? NUM_ORDER_TO { get; set; }
        public long? NUM_ORDER__EQUAL { get; set; }
        public long? SALE_TYPE_ID { get; set; }
        public long? BILL_TYPE_ID { get; set; }
        public long? ACCOUNT_BOOK_ID { get; set; }
        public long? CASHOUT_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? DEBT_BILL_ID { get; set; }
        public long? CASHIER_ROOM_ID { get; set; }

        public bool? HAS_CASHOUT { get; set; }
        public bool? HAS_NATIONAL_TRANSACTION_CODE { get; set; }
        public bool? BILL_TYPE_IS_NULL_OR_EQUAL_1 { get; set; }
        public bool? HAS_SALE_TYPE_ID { get; set; }
        public bool? IS_CANCEL { get; set; }
        public bool? IS_DEBT_COLLECTION { get; set; }
        public bool? HAS_DEBT_BILL_ID { get; set; }
        public bool? HAS_INVOICE_CODE { get; set; }

        public string TRANSACTION_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string ACCOUNT_BOOK_CODE__EXACT { get; set; }
        public string NATIONAL_TRANSACTION_CODE__EXACT { get; set; }
        public string SYMBOL_CODE__EXACT { get; set; }
        public string TEMPLATE_CODE__EXACT { get; set; }
        public string ACCOUNT_BOOK_NAME { get; set; }
        public string INVOICE_CODE__EXACT { get; set; }
        public string TRANS_REQ_CODE__EXACT { get; set; }

        public HisTransactionViewFilter()
            : base()
        {

        }
    }
}
