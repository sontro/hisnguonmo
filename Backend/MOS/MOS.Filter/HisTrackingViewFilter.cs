
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTrackingViewFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public long? TRACKING_TIME_FROM { get; set; }
        public long? TRACKING_TIME_TO { get; set; }
        public long? DEPARTMENT_ID { get; set; }

        public List<long> TREATMENT_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }

        public HisTrackingViewFilter()
            : base()
        {
        }
    }
}
