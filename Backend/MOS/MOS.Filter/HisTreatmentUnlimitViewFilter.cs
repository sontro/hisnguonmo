
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentUnlimitViewFilter : FilterBase
    {
        public long? UNLIMIT_TYPE_ID { get; set; }
        public long? TREATMENT_ID { get; set; }

        public List<long> UNLIMIT_TYPE_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public HisTreatmentUnlimitViewFilter()
            : base()
        {
        }
    }
}
