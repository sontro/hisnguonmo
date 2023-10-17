
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPatientTypeRoomViewFilter : FilterBase
    {
        public long? ROOM_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public long? ROOM_TYPE_ID { get; set; }

        public List<long> ROOM_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }
        public List<long> ROOM_TYPE_IDs { get; set; }

        public string ROOM_CODE__EXACT { get; set; }
        public string PATIENT_TYPE_CODE__EXACT { get; set; }
        public string ROOM_TYPE_CODE__EXACT { get; set; }

        public HisPatientTypeRoomViewFilter()
            : base()
        {
        }
    }
}
