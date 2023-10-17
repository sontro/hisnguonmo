
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisCarerCardBorrowViewFilter : FilterBase
    {
        public long? CARER_CARD_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? BORROW_TIME__FROM { get; set; }
        public long? BORROW_TIME__TO { get; set; }
        public long? GIVE_BACK_TIME__FROM { get; set; }
        public long? GIVE_BACK_TIME__TO { get; set; }

        public List<long> CARER_CARD_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public bool? HAS_GIVE_BACK_TIME { get; set; }
        public bool? IS_LOST { get; set; }

        public string TDL_PATIENT_NAME { get; set; }

        public string CARER_CARD_NUMBER__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string IN_CODE__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }

        public HisCarerCardBorrowViewFilter()
            : base()
        {
        }
    }
}
