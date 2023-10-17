
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBcsMetyReqDtFilter : FilterBase
    {
        public long? EXP_MEST_MEDICINE_ID { get; set; }
        public long? EXP_MEST_METY_REQ_ID { get; set; }

        public List<long> EXP_MEST_MEDICINE_IDs { get; set; }
        public List<long> EXP_MEST_METY_REQ_IDs { get; set; }

        public HisBcsMetyReqDtFilter()
            : base()
        {
        }
    }
}
