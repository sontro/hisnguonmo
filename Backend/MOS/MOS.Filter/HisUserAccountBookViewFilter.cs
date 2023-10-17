
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisUserAccountBookViewFilter : FilterBase
    {
        public long? ACCOUNT_BOOK_ID { get; set; }
        public List<long> ACCOUNT_BOOK_IDs { get; set; }

        public string LOGINNAME__EXACT { get; set; }
        public string ACCOUNT_BOOK_CODE__EXACT { get; set; }
        public string TEMPLATE_CODE__EXACT { get; set; }
        public string SYMBOL_CODE__EXACT { get; set; }

        public bool? IS_OUT_OF_BILL { get; set; }
        public bool? FOR_DEPOSIT { get; set; }
        public bool? FOR_REPAY { get; set; }
        public bool? FOR_BILL { get; set; }

        public short? ACCOUNT_BOOK_IS_ACTIVE { get; set; }

        public HisUserAccountBookViewFilter()
            : base()
        {
        }
    }
}
