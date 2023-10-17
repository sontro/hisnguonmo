
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPrepareFilter : FilterBase
    {
        public string PREPARE_CODE__EXACT { get; set; }
        public string REQ_LOGINNAME__EXACT { get; set; }
        public string APPROVAL_LOGINNAME__EXACT { get; set; }

        public long? TREATMENT_ID { get; set; }

        public List<long> TREATMENT_IDs { get; set; }

        public long? FROM_TIME_FROM { get; set; }
        public long? FROM_TIME_TO { get; set; }

        public long? TO_TIME_FROM { get; set; }
        public long? TO_TIME_TO { get; set; }

        public long? APPROVAL_TIME_FROM { get; set; }
        public long? APPROVAL_TIME_TO { get; set; }

        public bool? IS_APPROVE { get; set; }

        public HisPrepareFilter()
            : base()
        {
        }
    }
}
