
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisVaexVaerViewFilter : FilterBase
    {
        public long? VACCINATION_EXAM_ID { get; set; }
        public long? VACC_EXAM_RESULT_ID { get; set; }

        public List<long> VACCINATION_EXAM_IDs { get; set; }
        public List<long> VACC_EXAM_RESULT_IDs { get; set; }

        public HisVaexVaerViewFilter()
            : base()
        {
        }
    }
}
