
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceRereTimeViewFilter : FilterBase
    {
        public long? SERVICE_ID { get; set; }
        public List<long> SERVICE_IDs { get; set; }

        public HisServiceRereTimeViewFilter()
            : base()
        {
        }
    }
}
