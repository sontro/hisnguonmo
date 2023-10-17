
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAssessmentMemberFilter : FilterBase
    {
        public long? MEDICAL_ASSESSMENT_ID { get; set; }
        public List<long> MEDICAL_ASSESSMENT_IDs { get; set; }

        public HisAssessmentMemberFilter()
            : base()
        {
        }
    }
}
