using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Filter
{
    public class HisKskContractViewFilter : FilterBase
    {
        public long? WORK_PLACE_ID { get; set; }

        public long? CONTRACT_DATE_FROM { get; set; }
        public long? CONTRACT_DATE_TO { get; set; }
        public long? EFFECT_DATE_FROM { get; set; }
        public long? EFFECT_DATE_TO { get; set; }
        public long? EXPIRY_DATE_FROM { get; set; }
        public long? EXPIRY_DATE_TO { get; set; }

        public List<long> WORK_PLACE_IDs { get; set; }

        public HisKskContractViewFilter()
            : base()
        {
        }
    }
}
