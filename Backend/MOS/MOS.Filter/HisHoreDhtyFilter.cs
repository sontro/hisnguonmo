
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisHoreDhtyFilter : FilterBase
    {
        public long? HOLD_RETURN_ID { get; set; }
        public long? DOC_HOLD_TYPE_ID { get; set; }

        public List<long> HOLD_RETURN_IDs { get; set; }
        public List<long> DOC_HOLD_TYPE_IDs { get; set; }

        public HisHoreDhtyFilter()
            : base()
        {
        }
    }
}
