
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRoomSaroFilter : FilterBase
    {

        public long? SAMPLE_ROOM_ID { get; set; }
        public long? ROOM_ID { get; set; }

        public List<long> SAMPLE_ROOM_IDs { get; set; }
        public List<long> ROOM_IDs { get; set; }

        public HisRoomSaroFilter()
            : base()
        {
        }
    }
}
