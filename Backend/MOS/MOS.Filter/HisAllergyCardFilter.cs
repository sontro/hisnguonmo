
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAllergyCardFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }

        public List<long> TREATMENT_IDs { get; set; }

        public long? ISSUE_TIME_FROM { get; set; }
        public long? ISSUE_TIME_TO { get; set; }

        public string DIAGNOSE_LOGINNAME__EXACT { get; set; }

        public HisAllergyCardFilter()
            : base()
        {
        }
    }
}
