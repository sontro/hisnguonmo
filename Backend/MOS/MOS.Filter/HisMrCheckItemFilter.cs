using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisMrCheckItemFilter : FilterBase
    {
        public long? CHECK_ITEM_TYPE_ID { get; set; }
        public List<long> CHECK_ITEM_TYPE_IDs { get; set; }

        public HisMrCheckItemFilter()
            : base()
        {
        }
    }
}
