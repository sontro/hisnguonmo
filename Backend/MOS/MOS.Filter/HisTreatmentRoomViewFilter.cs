
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentRoomViewFilter : FilterBase
    {
        public long? BED_ROOM_ID { get; set; }

        public List<long> BED_ROOM_IDs { get; set; }

        public HisTreatmentRoomViewFilter()
            : base()
        {
        }
    }
}
