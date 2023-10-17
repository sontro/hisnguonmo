
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisSereServRationViewFilter : FilterBase
    {
        public long? SERVICE_ID { get; set; }
        public long? SERVICE_REQ_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public long? SERVICE_UNIT_ID { get; set; }
        public long? RATION_GROUP_ID { get; set; }
        public long? TRACKING_ID { get; set; }

        public List<long> SERVICE_IDs { get; set; }
        public List<long> SERVICE_REQ_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }
        public List<long> SERVICE_UNIT_IDs { get; set; }
        public List<long> RATION_GROUP_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> TRACKING_IDs { get; set; }

        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }

        public HisSereServRationViewFilter()
            : base()
        {
        }
    }
}
