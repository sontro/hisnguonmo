
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisEventsCausesDeathFilter : FilterBase
    {
        public long? SEVERE_ILLNESS_INFO_ID { get; set; }
        public List<long> SEVERE_ILLNESS_INFO_IDs { get; set; }

        public HisEventsCausesDeathFilter()
            : base()
        {
        }
    }
}
