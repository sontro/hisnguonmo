
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineTypeAcinFilter : FilterBase
    {
        public long? ACTIVE_INGREDIENT_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }

        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> ACTIVE_INGREDIENT_IDs { get; set; }

        public HisMedicineTypeAcinFilter()
            : base()
        {
        }
    }
}
