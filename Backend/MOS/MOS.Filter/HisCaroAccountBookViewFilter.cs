
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisCaroAccountBookViewFilter : FilterBase
    {
        public long? ACCOUNT_BOOK_ID { get; set; }
        public List<long> ACCOUNT_BOOK_IDs { get; set; }
        public long? CASHIER_ROOM_ID { get; set; }
        public List<long> CASHIER_ROOM_IDs { get; set; }

        public string ACCOUNT_BOOK_CODE__EXACT { get; set; }

        public bool? FOR_DEPOSIT { get; set; }
        public bool? FOR_REPAY { get; set; }
        public bool? FOR_BILL { get; set; }
        public bool? ACCOUNT_BOOK_IS_ACTIVE { get; set; }
        public bool? IS_OUT_OF_BILL { get; set; }

        public HisCaroAccountBookViewFilter()
            : base()
        {
        }
    }
}
