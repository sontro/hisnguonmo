
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisVaexVaerFilter : FilterBase
    {
        public long? VACCINATION_EXAM_ID { get; set; }
        public long? VACC_EXAM_RESULT_ID { get; set; }

        public List<long> VACCINATION_EXAM_IDs { get; set; }
        public List<long> VACC_EXAM_RESULT_IDs { get; set; }

        public HisVaexVaerFilter()
            : base()
        {
        }
    }
}
