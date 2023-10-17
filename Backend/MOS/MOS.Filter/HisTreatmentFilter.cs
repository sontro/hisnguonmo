
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentFilter : FilterBase
    {
        public List<long> OWE_TYPE_IDs { get; set; }
        public List<long> DEATH_CAUSE_IDs { get; set; }
        public List<long> DEATH_WITHIN_IDs { get; set; }
        public List<long> TRAN_PATI_FORM_IDs { get; set; }
        public List<long> EMERGENCY_WTIME_IDs { get; set; }
        public List<long> TRAN_PATI_REASON_IDs { get; set; }
        public List<long> BRANCH_IDs { get; set; }
        public List<long> TDL_KSK_CONTRACT_IDs { get; set; }
        public List<long> DATA_STORE_ID_NULL__OR__INs { get; set; }
        public List<long> TDL_PATIENT_TYPE_IDs { get; set; }
        public List<long> MEDI_RECORD_TYPE_IDs { get; set; }
        public List<long> MEDI_RECORD_IDs { get; set; }
        public List<long> APPROVAL_STORE_STT_IDs { get; set; }
        public List<long> PATIENT_CLASSIFY_IDs { get; set; }
        public List<long> TDL_TREATMENT_TYPE_IDs { get; set; }

        public List<long> XML4210_RESULTs { get; set; }
        public List<long> COLLINEAR_XML4210_RESULTs { get; set; }

        public long? BRANCH_ID { get; set; }
        public long? TRAN_PATI_FORM_ID { get; set; }
        public long? TRAN_PATI_REASON_ID { get; set; }
        public long? DEATH_WITHIN_ID { get; set; }
        public long? DEATH_CAUSE_ID { get; set; }
        public long? PATIENT_ID { get; set; }
        public long? TREATMENT_END_TYPE_ID { get; set; }
        public long? TREATMENT_RESULT_ID { get; set; }
        public long? IN_TIME_FROM { get; set; }
        public long? IN_TIME_TO { get; set; }
        public long? OWE_TYPE_ID { get; set; }
        public long? EMERGENCY_WTIME_ID { get; set; }
        public long? MEDI_RECORD_TYPE_ID { get; set; }
        public long? MEDI_RECORD_ID { get; set; }
        public long? APPROVAL_STORE_STT_ID { get; set; }
        public long? ID__NOT_EQUAL { get; set; }
        public long? HEIN_LOCK_TIME_FROM { get; set; }
        public long? HEIN_LOCK_TIME_TO { get; set; }

        public string EXTRA_END_CODE__END_WITH { get; set; }
        public string EXTRA_END_CODE__START_WITH { get; set; }
        public string STORE_CODE__END_WITH { get; set; }
        public string STORE_CODE__START_WITH { get; set; }
        public string IN_CODE__END_WITH { get; set; }
        public string IN_CODE__EXACT { get; set; }
        public string IN_CODE__START_WITH { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string APPOINTMENT_CODE__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }
        public long? IN_DATE { get; set; }
        public bool? IS_OUT { get; set; }
        public bool? IS_YDT_UPLOAD { get; set; }
        public bool? IS_LOCK_HEIN { get; set; }
        public bool? IS_PAUSE { get; set; }
        public string ICD_CODE_OR_ICD_SUB_CODE { get; set; }
        public bool? IS_LOCK_FEE { get; set; }
        public long? TDL_KSK_CONTRACT_ID { get; set; }
        public string HRM_KSK_CODE__EXACT { get; set; }
        public string STORE_CODE__EXACT { get; set; }

        public long? XML4210_RESULT { get; set; }
        public long? COLLINEAR_XML4210_RESULT { get; set; }

        public long? XML4210_RESULT__OR__COLLINEAR_XML4210_RESULT { get; set; }

        public long? FEE_LOCK_TIME__FROM { get; set; }
        public long? FEE_LOCK_TIME__TO { get; set; }

        public bool? IS_APPROVE_FINISH { get; set; }
        public bool? IS_KSK_APPROVE { get; set; }

        public long? APPOINTMENT_PERIOD_ID { get; set; }
        public long? DOCUMENT_BOOK_ID { get; set; }
        public long? PATIENT_CLASSIFY_ID { get; set; }

        public long? TDL_TREATMENT_TYPE_ID { get; set; }
        public long? OUT_DATE { get; set; }
        public string DOCTOR_LOGINNAME__EXACT { get; set; }
        public long? TREATMENT_END_TYPE_EXT_ID { get; set; }

        public long? OUT_DATE_FROM { get; set; }
        public long? OUT_DATE_TO { get; set; }
        public bool? HAS_TRAN_PATI_BOOK_NUMBER { get; set; }
        public long? OUT_TIME_FROM { get; set; }
        public long? OUT_TIME_TO { get; set; }
        public bool? HAS_XML4210_URL { get; set; }
        public bool? IS_BHYT_PATIENT_TYPE { get; set; }
        public bool? IS_NOI_TRU_TREATMENT_TYPE { get; set; }
        public bool? HAS_AUTO_CREATE_RATION { get; set; }
        public bool? HAS_MEDI_RECORD { get; set; }
        public bool? HAS_STORE_CODE { get; set; }
        public long? DEATH_SYNC_RESULT_TYPE { get; set; }
        public long? DEATH_TIME_FROM { get; set; }
        public long? DEATH_TIME_TO { get; set; }
        public long? APPOINTMENT_DATE { get; set; }
        public bool? HAS_MOBILE { get; set; }

        public HisTreatmentFilter()
            : base()
        {
        }
    }
}
