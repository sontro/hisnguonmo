
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisExroRoomFilter : FilterBase
    {
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? ROOM_ID { get; set; }

        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> ROOM_IDs { get; set; }

        public bool? IS_HOLD_ORDER { get; set; }
        public bool? IS_ALLOW_REQUEST { get; set; }

        public HisExroRoomFilter()
            : base()
        {
        }
    }
}
