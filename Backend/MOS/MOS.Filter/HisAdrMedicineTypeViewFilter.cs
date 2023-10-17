
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAdrMedicineTypeViewFilter : FilterBase
    {
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? ADR_ID { get; set; }
        public long? MANUFACTURER_ID { get; set; }

        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> ADR_IDs { get; set; }
        public List<long> MANUFACTURER_IDs { get; set; }

        public HisAdrMedicineTypeViewFilter()
            : base()
        {
        }
    }
}
