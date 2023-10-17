
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineTypeAcinViewFilter : FilterBase
    {
        public string MEDICINE_TYPE_CODE__EXACT { get; set; }
        public string ACTIVE_INGREDIENT_CODE__EXACT { get; set; }

        public long? MEDICINE_TYPE_ID { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }

        public long? ACTIVE_INGREDIENT_ID { get; set; }
        public List<long> ACTIVE_INGREDIENT_IDs { get; set; }

        public bool? IS_APPROVAL_REQUIRED { get; set; }

        public HisMedicineTypeAcinViewFilter()
            : base()
        {
        }
    }
}
