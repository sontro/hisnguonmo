
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentBedRoomView1Filter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public long? BED_ROOM_ID { get; set; }
        public long? ADD_TIME_TO { get; set; }
        public long? ADD_TIME_FROM { get; set; }
        public long? REMOVE_TIME_FROM { get; set; }
        public long? BED_ID { get; set; }

        public bool? IS_IN_ROOM { get; set; }
        public string KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME__PATIENT_CODE { get; set; }
        public string KEYWORD__PATIENT_NAME__TREATMENT_CODE__BED_NAME { get; set; }

        public List<long> TREATMENT_IDs { get; set; }
        public List<long> BED_IDs { get; set; }

        public string TREATMENT_CODE__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public long? DEPARTMENT_ID { get; set; }

        public bool? HAS_CO_TREATMENT_ID { get; set; }
        public bool? IS_APPROVE_FINISH { get; set; }

        public HisTreatmentBedRoomView1Filter()
            : base()
        {
        }
    }
}
