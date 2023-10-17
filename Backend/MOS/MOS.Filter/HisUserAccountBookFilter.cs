
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisUserAccountBookFilter : FilterBase
    {
        public long? ACCOUNT_BOOK_ID { get; set; }
        public List<long> ACCOUNT_BOOK_IDs { get; set; }

        public string LOGINNAME__EXACT { get; set; }

        public HisUserAccountBookFilter()
            : base()
        {
        }
    }
}
