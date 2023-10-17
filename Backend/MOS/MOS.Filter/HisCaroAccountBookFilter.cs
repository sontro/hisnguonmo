
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisCaroAccountBookFilter : FilterBase
    {
        public long? ACCOUNT_BOOK_ID { get; set; }
        public List<long> ACCOUNT_BOOK_IDs { get; set; }
        public long? CASHIER_ROOM_ID { get; set; }
        public List<long> CASHIER_ROOM_IDs { get; set; }

        public HisCaroAccountBookFilter()
            : base()
        {
        }
    }
}
