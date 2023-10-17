
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRoomViewFilter : FilterBase
    {
        public long? DEPARTMENT_ID { get; set; }
        public long? ROOM_TYPE_ID { get; set; }
        public long? SPECIALITY_ID { get; set; }
        public List<long> SPECIALITY_IDs { get; set; }
        public List<string> ROOM_CODEs { get; set; }

        public HisRoomViewFilter()
            : base()
        {

        }
    }
}
