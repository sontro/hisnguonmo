
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisSeseTransReqFilter : FilterBase
    {
        public long? TRANS_REQ_ID { get; set; }

        public List<long> SERE_SERV_IDs { get; set; }
        public List<long> TRANS_REQ_IDs { get; set; }

        public HisSeseTransReqFilter()
            : base()
        {
        }
    }
}
