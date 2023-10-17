
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisKskPeriodDriverFilter : FilterBase
    {
        public long? SERVICE_REQ_ID { get; set; }
        public long? LICENSE_CLASS_ID { get; set; }
        public List<long> SERVICE_REQ_IDs { get; set; }
        public List<long> LICENSE_CLASS_IDs { get; set; }

        public HisKskPeriodDriverFilter()
            : base()
        {
        }
    }
}
