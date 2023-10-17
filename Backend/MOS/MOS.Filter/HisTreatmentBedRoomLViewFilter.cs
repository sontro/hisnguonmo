
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentBedRoomLViewFilter : FilterBase
    {
        public long? ADD_TIME_TO { get; set; }
        public long? ADD_TIME_FROM { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? BED_ROOM_ID { get; set; }
        public long? REMOVE_TIME_FROM { get; set; }
        public bool? IS_IN_ROOM { get; set; }
        public string KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE { get; set; }
        public string KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME { get; set; }
        public bool? IS_PAUSE { get; set; }

        public bool? HAS_CO_TREATMENT_ID { get; set; }
        public bool? IS_APPROVE_FINISH { get; set; }

        public List<long> TREATMENT_IDs { get; set; }
        public List<long> TREATMENT_TYPE_IDs { get; set; }
        public List<long> BED_ROOM_IDs { get; set; }
        public List<long> TREATMENT_ROOM_IDs { get; set; }
        public List<long> PATIENT_CLASSIFY_IDs { get; set; }
        public long? TREATMENT_ROOM_ID { get; set; }
        public bool? HAS_OUT_TIME { get; set; }
        public long? PATIENT_CLASSIFY_ID { get; set; }
        public long? TREATMENT_TYPE_ID { get; set; }
        public long? OBSERVED_TIME_BETWEEN { get; set; }

        public HisTreatmentBedRoomLViewFilter()
            : base()
        {
        }
    }
}
