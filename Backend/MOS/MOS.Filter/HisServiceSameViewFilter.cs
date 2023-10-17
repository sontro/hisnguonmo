
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceSameViewFilter : FilterBase
    {
        public long? SERVICE_ID { get; set; }
        public long? SAME_ID { get; set; }
        public long? SERVICE_ID__OR__SAME_ID { get; set; }
        public List<long> SERVICE_ID__OR__SAME_IDs { get; set; }
        public List<long> SERVICE_IDs { get; set; }
        public List<long> SAME_IDs { get; set; }

        public HisServiceSameViewFilter()
            : base()
        {
        }
    }
}
