using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisMrChecklistFilter : FilterBase
    {
        public long? MR_CHECK_ITEM_ID { get; set; }
        public List<long> MR_CHECK_ITEM_IDs { get; set; }
        public long? MR_CHECK_SUMMARY_ID { get; set; }
        public List<long> MR_CHECK_SUMMARY_IDs { get; set; }

        public HisMrChecklistFilter()
            : base()
        {
        }
    }
}
