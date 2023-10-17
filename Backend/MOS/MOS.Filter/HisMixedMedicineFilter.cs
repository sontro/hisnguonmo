using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisMixedMedicineFilter : FilterBase
    {
        public long? INFUSION_ID { get; set; }
        public List<long> INFUSION_IDs { get; set; }

        public HisMixedMedicineFilter()
            : base()
        {
        }
    }
}
