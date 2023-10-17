
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBcsMatyReqDtFilter : FilterBase
    {
        public long? EXP_MEST_MATERIAL_ID { get; set; }
        public long? EXP_MEST_MATY_REQ_ID { get; set; }

        public List<long> EXP_MEST_MATERIAL_IDs { get; set; }
        public List<long> EXP_MEST_MATY_REQ_IDs { get; set; }

        public HisBcsMatyReqDtFilter()
            : base()
        {
        }
    }
}
