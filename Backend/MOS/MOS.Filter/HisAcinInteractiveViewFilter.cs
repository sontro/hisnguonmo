using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisAcinInteractiveViewFilter : FilterBase
    {
        public long? INTERACTIVE_GRADE_ID { get; set; }
        public List<long> INTERACTIVE_GRADE_IDs { get; set; }

        public HisAcinInteractiveViewFilter()
            : base()
        {
        }
    }
}
