
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAllergyCardViewFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public long? PATIENT_ID { get; set; }

        public List<long> TREATMENT_IDs { get; set; }
        public List<long> PATIENT_IDs { get; set; }

        public long? ISSUE_TIME_FROM { get; set; }
        public long? ISSUE_TIME_TO { get; set; }

        public string DIAGNOSE_LOGINNAME__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }

        public HisAllergyCardViewFilter()
            : base()
        {
        }
    }
}
