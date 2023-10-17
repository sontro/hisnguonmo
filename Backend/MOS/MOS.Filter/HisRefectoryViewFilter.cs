
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRefectoryViewFilter : FilterBase
    {
        public string REFECTORY_CODE__EXACT { get; set; }
        public string DEPARTMENT_CODE__EXACT { get; set; }
        public string BRANCH_CODE__EXACT { get; set; }
        
        public long? ROOM_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? ROOM_TYPE_ID { get; set; }

        public List<long> ROOM_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> ROOM_TYPE_IDs { get; set; }

        public HisRefectoryViewFilter()
            : base()
        {
        }
    }
}
