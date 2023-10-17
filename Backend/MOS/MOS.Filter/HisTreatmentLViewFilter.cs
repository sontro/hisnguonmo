
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentLViewFilter
    {
        protected static readonly long NEGATIVE_ID = -1;
        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }
        public string KEY_WORD { get; set; }
        public long? ID { get; set; }
        public short? IS_ACTIVE { get; set; }
        public long? CREATE_TIME_FROM { get; set; }
        public long? CREATE_TIME_TO { get; set; }
        public long? DOB_FROM { get; set; }
        public long? DOB_TO { get; set; }
        public long? IN_TIME_FROM { get; set; }
        public long? IN_TIME_TO { get; set; }
        public long? OUT_TIME_FROM { get; set; }
        public long? OUT_TIME_TO { get; set; }
        public bool? IS_LOCK_HEIN { get; set; }
        public bool? IS_PAUSE { get; set; }
        public bool? IS_OUT { get; set; }
        public bool? IS_RESTRICTED_KSK { get; set; }
        public string KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }

        public string PATIENT_NAME { get; set; }
        public List<string> ICD_CODE_OR_ICD_SUB_CODEs { get; set; }
        public string ICD_CODE_OR_ICD_SUB_CODE { get; set; }

        public HisTreatmentLViewFilter()
            : base()
        {
        }
    }
}
