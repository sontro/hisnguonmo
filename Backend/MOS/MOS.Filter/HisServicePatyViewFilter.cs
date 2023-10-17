using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisServicePatyViewFilter : FilterBase
    {
        public bool? IN_ACTIVE_TIME { get; set; }
        public bool? SERVICE_IS_ACTIVE { get; set; }
        public long? TREATMENT_TIME { get; set; }
        public long? SERVICE_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }
        public long? BRANCH_ID { get; set; }
        public long? PACKAGE_ID { get; set; }

        public List<long> SERVICE_IDs { get; set; }
        public List<long> SERVICE_TYPE_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }
        public List<long> PACKAGE_IDs { get; set; }

        public HisServicePatyViewFilter()
            : base()
        {

        }
    }
}
