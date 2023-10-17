using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisKskAccessFilter : FilterBase
    {
        public long? EMPLOYEE_ID { get; set; }
        public long? KSK_CONTRACT_ID { get; set; }

        public List<long> EMPLOYEE_IDs { get; set; }
        public List<long> KSK_CONTRACT_IDs { get; set; }

        public HisKskAccessFilter()
            : base()
        {
        }
    }
}
