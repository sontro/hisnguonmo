
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisHoreHandoverViewFilter : FilterBase
    {
        public long? SEND_ROOM_ID { get; set; }
        public long? HORE_HANDOVER_STT_ID { get; set; }
        public long? RECEIVE_ROOM_ID { get; set; }

        public List<long> SEND_ROOM_IDs { get; set; }
        public List<long> HORE_HANDOVER_STT_IDs { get; set; }
        public List<long> RECEIVE_ROOM_IDs { get; set; }

        public string HORE_HANDOVER_CODE__EXACT { get; set; }
        public string SEND_LOGINNAME__EXACT { get; set; }
        public string RECEIVE_LOGINNAME__EXACT { get; set; }

        public long? SEND_OR_RECEIVE_ROOM_ID { get; set; }

        public HisHoreHandoverViewFilter()
            : base()
        {
        }
    }
}
