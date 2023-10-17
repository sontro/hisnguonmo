
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisCarerCardBorrowFilter : FilterBase
    {
        public long? CARER_CARD_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? HAS_NO__OR__LOWER_THAN_GIVE_BACK_TIME { get; set; }

        public List<long> CARER_CARD_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public bool? HAS_GIVE_BACK_TIME { get; set; }

        public HisCarerCardBorrowFilter()
            : base()
        {
        }
    }
}
