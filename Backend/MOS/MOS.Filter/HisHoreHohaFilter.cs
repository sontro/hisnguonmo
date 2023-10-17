
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisHoreHohaFilter : FilterBase
    {
        public long? HOLD_RETURN_ID { get; set; }
        public long? HORE_HANDOVER_ID { get; set; }
        public List<long> HOLD_RETURN_IDs { get; set; }
        public List<long> HORE_HANDOVER_IDs { get; set; }

        public HisHoreHohaFilter()
            : base()
        {
        }
    }
}
