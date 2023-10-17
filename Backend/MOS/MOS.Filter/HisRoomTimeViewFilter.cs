
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRoomTimeViewFilter : FilterBase
    {
        public long? ROOM_ID { get; set; }
        public List<long> ROOM_IDs { get; set; }
        public long? ROOM_TYPE_ID { get; set; }
        public List<long> ROOM_TYPE_IDs { get; set; }

        public HisRoomTimeViewFilter()
            : base()
        {
        }
    }
}
