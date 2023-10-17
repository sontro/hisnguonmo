
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisContactFilter : FilterBase
    {
        public long? CONTACT_TIME_FROM { get; set; }
        public long? CONTACT_TIME_TO { get; set; }
        public long? CONTACT_POINT1_ID { get; set; }
        public long? CONTACT_POINT2_ID { get; set; }
        public long? CONTACT_POINT1_ID__OR__CONTACT_POINT2_ID { get; set; }
        public List<long> CONTACT_POINT1_ID__OR__CONTACT_POINT2_IDs { get; set; }

        public HisContactFilter()
            : base()
        {
        }
    }
}
