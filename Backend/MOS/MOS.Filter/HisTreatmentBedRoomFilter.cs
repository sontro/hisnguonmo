
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentBedRoomFilter : FilterBase
    {
        public List<long> BED_IDs { get; set; }

        public long? BED_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? BED_ROOM_ID { get; set; }
        public bool? IS_IN_ROOM { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> BED_ROOM_IDs { get; set; }

        public long? CO_TREATMENT_ID { get; set; }
        public List<long> CO_TREATMENT_IDs { get; set; }

        public bool? HAS_CO_TREATMENT_ID { get; set; }
        public long? TREATMENT_ROOM_ID { get; set; }

        public HisTreatmentBedRoomFilter()
            : base()
        {
        }
    }
}
