
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentLView3Filter
    {
        protected static readonly long NEGATIVE_ID = -1;
        public long? ID { get; set; }
        public long? MEDI_RECORD_ID { get; set; }
        public long? END_DEPARTMENT_ID { get; set; }
        public long? LAST_DEPARTMENT_ID { get; set; }
        public long? TDL_PATIENT_TYPE_ID { get; set; }
        public long? TDL_TREATMENT_TYPE_ID { get; set; }

        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }
        public string KEY_WORD { get; set; }
        
        public string TREATMENT_CODE__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }

        public bool? IS_PAUSE { get; set; }
        public bool? IS_LOCK_HEIN { get; set; }
        public bool? IS_STORED { get; set; }

        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }
        public long? OUT_DATE_FROM { get; set; }
        public long? OUT_DATE_TO { get; set; }

        public List<long> END_DEPARTMENT_IDs { get; set; }
        public List<long> LAST_DEPARTMENT_IDs { get; set; }
        public List<long> TDL_PATIENT_TYPE_IDs { get; set; }
        public List<long> TDL_TREATMENT_TYPE_IDs { get; set; }

        public long? APPROVAL_STORE_STT_ID__NULL_OR_EQUAL { get; set; }
        public bool? HAS_APPROVAL_STORE_STT_ID { get; set; }
        public long? APPROVAL_STORE_STT_ID  { get; set; }
        public bool? IS_RESTRICTED_KSK { get; set; }

        public HisTreatmentLView3Filter()
            : base()
        {
        }
    }
}
