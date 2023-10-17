
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceConditionViewFilter : FilterBase
    {
        public long? SERVICE_ID { get; set; }

        public List<long> SERVICE_IDs { get; set; }

        public HisServiceConditionViewFilter()
            : base()
        {
        }
    }
}
