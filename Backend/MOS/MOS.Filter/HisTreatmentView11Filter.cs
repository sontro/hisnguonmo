using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisTreatmentView11Filter : FilterBase
    {
        public string PATIENT_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string MR_STORE_CODE__EXACT { get; set; }
        public long? STORE_DATE_FROM { get; set; }
        public long? STORE_DATE_TO { get; set; }
        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }
        public long? OUT_DATE_FROM { get; set; }
        public long? OUT_DATE_TO { get; set; }
        public long? LAST_DEPARTMENT_ID { get; set; }
        public long? END_DEPARTMENT_ID { get; set; }
        public long? RECORD_INSPECTION_STT_ID { get; set; }
        public bool? IS_PAUSE { get; set; }
        public bool? HAS_MEDI_RECORD { get; set; }
        public bool? HAS_RECORD_INSPECTION_STT_ID { get; set; }
        public long? BRANCH_ID { get; set; }

        public HisTreatmentView11Filter()
            : base()
        {
        }
    }
}
