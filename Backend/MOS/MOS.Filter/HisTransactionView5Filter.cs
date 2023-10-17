using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisTransactionView5Filter : FilterBase
    {
        public bool? IS_CANCEL { get; set; }
        public long? ACCOUNT_BOOK_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public List<long> TRANSACTION_TYPE_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public string TRANSACTION_CODE__EXACT { get; set; }
        public string ACCOUNT_BOOK_CODE__EXACT { get; set; }

        public bool? IS_DEBT_COLLECTION { get; set; }
        public bool? HAS_DEBT_BILL_ID { get; set; }

        public HisTransactionView5Filter()
            : base()
        {

        }
    }
}
