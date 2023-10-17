
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBcsMetyReqReqFilter : FilterBase
    {
        public long? EXP_MEST_METY_REQ_ID { get; set; }
        public long? PRE_EXP_MEST_METY_REQ_ID { get; set; }

        public List<long> EXP_MEST_METY_REQ_IDs { get; set; }
        public List<long> PRE_EXP_MEST_METY_REQ_IDs { get; set; }


        public HisBcsMetyReqReqFilter()
            : base()
        {
        }
    }
}
