using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisAcinInteractiveFilter : FilterBase
    {
        public long? ACTIVE_INGREDIENT_ID { get; set; }
        public long? ACTIVE_INGREDIENT_CONFLICT_ID { get; set; }
        public long? ACTIVE_INGREDIENT_ID__OR__ACTIVE_INGREDIENT_CONFLICT_ID { get; set; }
        public long? INTERACTIVE_GRADE_ID { get; set; }
        public List<long> INTERACTIVE_GRADE_IDs { get; set; }

        public HisAcinInteractiveFilter()
            : base()
        {
        }
    }
}
