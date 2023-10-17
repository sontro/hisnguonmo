using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisKskAccessViewFilter : FilterBase
    {
        public long? EMPLOYEE_ID { get; set; }
        public long? KSK_CONTRACT_ID { get; set; }
        public long? EFFECT_DATE { get; set; }
        public long? EXPIRY_DATE { get; set; }
        public long? EFFECT_DATE_FROM { get; set; }
        public long? EFFECT_DATE_TO { get; set; }
        public long? EXPIRY_DATE_FROM { get; set; }
        public long? EXPIRY_DATE_TO { get; set; }

        public string KSK_CONTRACT_CODE__EXACT { get; set; }
        public string LOGINNAME { get; set; }

        public List<long> EMPLOYEE_IDs { get; set; }
        public List<long> KSK_CONTRACT_IDs { get; set; }

        public HisKskAccessViewFilter()
            : base()
        {
        }
    }
}
