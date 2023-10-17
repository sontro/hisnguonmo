
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceMetyFilter : FilterBase
    {
        public List<long> SERVICE_IDs { get; set; }
        public long? SERVICE_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }

        public HisServiceMetyFilter()
            : base()
        {
        }
    }
}
