
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceRereTimeFilter : FilterBase
    {
        public long? SERVICE_ID { get; set; }
        public List<long> SERVICE_IDs { get; set; }

        public HisServiceRereTimeFilter()
            : base()
        {
        }
    }
}
