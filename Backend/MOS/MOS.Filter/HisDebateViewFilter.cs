
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisDebateViewFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? DEBATE_TIME_FROM { get; set; }
        public long? DEBATE_TIME_TO { get; set; }

        public string TREATMENT_CODE__EXACT { get; set; }
        public string INVITE_USER_LOGINNAME { get; set; }

        public List<long> TREATMENT_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }

        public HisDebateViewFilter()
            : base()
        {
        }
    }
}
