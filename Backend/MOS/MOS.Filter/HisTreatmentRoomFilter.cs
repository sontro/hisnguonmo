
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentRoomFilter : FilterBase
    {
        public long? BED_ROOM_ID { get; set; }
        public List<long> BED_ROOM_IDs { get; set; }

        public HisTreatmentRoomFilter()
            : base()
        {
        }
    }
}
