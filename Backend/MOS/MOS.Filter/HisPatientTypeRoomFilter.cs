
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPatientTypeRoomFilter : FilterBase
    {
        public long? ROOM_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }

        public List<long> ROOM_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }

        public HisPatientTypeRoomFilter()
            : base()
        {
        }
    }
}
