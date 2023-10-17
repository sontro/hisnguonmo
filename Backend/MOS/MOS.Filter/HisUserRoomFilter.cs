
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisUserRoomFilter : FilterBase
    {
        public string LOGINNAME__EXACT { get; set; }
        public long? ROOM_ID { get; set; }
        public long? ID__NOT_EQUAL { get; set; }

        public List<long> ROOM_IDs { get; set; }

        public HisUserRoomFilter()
            : base()
        {
        }
    }
}