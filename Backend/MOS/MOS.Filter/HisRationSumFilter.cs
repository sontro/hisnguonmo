
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRationSumFilter : FilterBase
    {
        public long? ROOM_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? RATION_SUM_STT_ID { get; set; }

        public List<long> ROOM_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> RATION_SUM_STT_IDs { get; set; }

        public long? REQ_DATE_FROM { get; set; }
        public long? REQ_DATE_TO { get; set; }

        public long? APPROVAL_DATE_FROM { get; set; }
        public long? APPROVAL_DATE_TO { get; set; }
        
        public HisRationSumFilter()
            : base()
        {
        }
    }
}
