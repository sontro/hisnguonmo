
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentViewFilter : FilterBase
    {
        public List<long> END_ROOM_IDs { get; set; }
        public List<long> DATA_STORE_IDs { get; set; }
        public List<long> END_DEPARTMENT_IDs { get; set; }
        public List<long> PATIENT_IDs { get; set; }
        public List<long> TDL_KSK_CONTRACT_IDs { get; set; }
        public List<long> BRANCH_IDs { get; set; }
        public List<string> ICD_CODE_OR_ICD_SUB_CODEs { get; set; }
        public List<long> DATA_STORE_ID_NULL__OR__INs { get; set; }
        public List<long> TDL_PATIENT_TYPE_IDs { get; set; }
        public List<long> MEDI_RECORD_IDs { get; set; } 

        public long? FEE_LOCK_TIME_FROM { get; set; }
        public long? FEE_LOCK_TIME_TO { get; set; }
        public long? DOB_FROM { get; set; }
        public long? DOB_TO { get; set; }
        public long? IN_TIME_FROM { get; set; }
        public long? IN_TIME_TO { get; set; }
        public long? STORE_TIME_FROM { get; set; }
        public long? STORE_TIME_TO { get; set; }
        public long? OUT_TIME_FROM { get; set; }
        public long? OUT_TIME_TO { get; set; }
        public bool? IS_LOCK_HEIN { get; set; }
        public bool? IS_PAUSE { get; set; }
        public bool? IS_OUT { get; set; }
        public long? PATIENT_ID { get; set; }
        public string KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME { get; set; }
        public string PATIENT_NAME { get; set; }
        public long? TREATMENT_END_TYPE_ID { get; set; }
        public bool? HAS_DATA_STORE { get; set; }
        
        public string PATIENT_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public bool? IS_CHRONIC { get; set; }
        public string TDL_HEIN_CARD_NUMBER__EXACT { get; set; }
        public long? CLINICAL_IN_TIME_FROM { get; set; }
        public long? CLINICAL_IN_TIME_TO { get; set; }
        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }
        public bool? IS_YDT_UPLOAD { get; set; }
        public long? TDL_KSK_CONTRACT_ID { get; set; }
        public string HRM_KSK_CODE__EXACT { get; set; }
        public long? BRANCH_ID { get; set; }
        public string ICD_CODE_OR_ICD_SUB_CODE { get; set; }
        public long? MEDI_RECORD_ID { get; set; }
        public long? DEATH_SYNC_RESULT_TYPE { get; set; }
        public long? DEATH_TIME_FROM { get; set; }
        public long? DEATH_TIME_TO { get; set; }
        public long? APPOINTMENT_DATE { get; set; }
        public bool? HAS_MOBILE { get; set; }
        public long? DEATH_ISSUED_DATE_FROM { get; set; }
        public long? DEATH_ISSUED_DATE_TO { get; set; }

        public HisTreatmentViewFilter()
            : base()
        {
        }
    }
}
