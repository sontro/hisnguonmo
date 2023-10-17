
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAllergenicViewFilter : FilterBase
    {
        public long? ALLERGY_CARD_ID { get; set; }
        public long? TDL_PATIENT_ID { get; set; }

        public List<long> ALLERGY_CARD_IDs { get; set; }
        public List<long> TDL_PATIENT_IDs { get; set; }

        public bool? IS_DOUBT { get; set; }
        public bool? IS_SURE { get; set; }
        public bool? HAS_MEDICINE_TYPE_ID { get; set; }

        public HisAllergenicViewFilter()
            : base()
        {
        }
    }
}
