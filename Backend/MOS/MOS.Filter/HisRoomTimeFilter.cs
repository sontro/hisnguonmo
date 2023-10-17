
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRoomTimeFilter : FilterBase
    {
        public long? ROOM_ID { get; set; }
        public List<long> ROOM_IDs { get; set; }

        public HisRoomTimeFilter()
            : base()
        {
        }
    }
}
