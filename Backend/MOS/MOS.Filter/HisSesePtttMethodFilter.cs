using System;
using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisSesePtttMethodFilter : FilterBase
    {
        public long? TDL_SERE_SERV_ID { get; set; }
        public long? TDL_SERVICE_REQ_ID { get; set; }
        public long? SERE_SERV_PTTT_ID { get; set; }

        public List<long> SERE_SERV_PTTT_IDs { get; set; }

        public HisSesePtttMethodFilter()
            : base()
        {
        }
    }
}
