
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRoomFilter : FilterBase
    {
        public long? DEPARTMENT_ID { get; set; }
        public long? ROOM_TYPE_ID { get; set; }
        public bool? IS_PAUSE { get; set; }
        public long? ROOM_GROUP_ID { get; set; }
        public long? SPECIALITY_ID { get; set; }
        public List<long> SPECIALITY_IDs { get; set; }

        public HisRoomFilter()
            : base()
        {
        }
    }
}
