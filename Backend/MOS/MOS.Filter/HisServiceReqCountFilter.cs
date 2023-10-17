
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceReqCountFilter
    {
        public long? INTRUCTION_DATE { get; set; }
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? EXE_DESK_ID { get; set; }
        public bool? IS_BHYT { get; set; }
        public string EXECUTE_LOGINNAME { get; set; }
        public List<long> SERVICE_REQ_STT_IDs { get; set; }
        public List<long> SERVICE_REQ_TYPE_IDs { get; set; }
        
        public HisServiceReqCountFilter()
            : base()
        {
        }
    }
}
