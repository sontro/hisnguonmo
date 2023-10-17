
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceRatiFilter : FilterBase
    {
        public long? SERVICE_ID { get; set; }
        public long? RATION_TIME_ID { get; set; }

        public List<long> SERVICE_IDs { get; set; }
        public List<long> RATION_TIME_IDs { get; set; }

        public HisServiceRatiFilter()
            : base()
        {
        }
    }
}
