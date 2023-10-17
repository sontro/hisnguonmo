
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineTypeRoomViewFilter : FilterBase
    {
        public long? ROOM_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }

        public List<long> ROOM_IDs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }

        public HisMedicineTypeRoomViewFilter()
            : base()
        {
        }
    }
}
