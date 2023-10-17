
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAdrMedicineTypeFilter : FilterBase
    {
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? ADR_ID { get; set; }

        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> ADR_IDs { get; set; }


        public HisAdrMedicineTypeFilter()
            : base()
        {
        }
    }
}
