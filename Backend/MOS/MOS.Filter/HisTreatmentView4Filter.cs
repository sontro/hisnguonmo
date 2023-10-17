
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentView4Filter : FilterBase
    {
        public List<long> END_ROOM_IDs { get; set; }
        public List<long> IN_DEPARTMENT_IDs { get; set; }
        public List<long> IN_ROOM_IDs { get; set; }
        public List<long> TDL_TREATMENT_TYPE_IDs { get; set; }
        public List<long> TREATMENT_RESULT_IDs { get; set; }
        public List<long> TREATMENT_END_TYPE_IDs { get; set; }
        public List<long> TDL_KSK_CONTRACT_IDs { get; set; }
        public List<long> DATA_STORE_ID_NULL__OR__INs { get; set; }
        public List<long> TDL_PATIENT_TYPE_IDs { get; set; }
        public List<long> APPROVAL_STORE_STT_IDs { get; set; }
        public List<long> LAST_DEPARTMENT_IDs { get; set; }
        public List<long> HOSPITALIZE_DEPARTMENT_IDs { get; set; }

        public long? APPROVAL_STORE_STT_ID { get; set; }
        public long? WAS_BEEN_DEPARTMENT_ID { get; set; }
        public long? FEE_LOCK_TIME_FROM { get; set; }
        public long? FEE_LOCK_TIME_TO { get; set; }
        public long? CLINICAL_IN_TIME_FROM { get; set; }
        public long? CLINICAL_IN_TIME_TO { get; set; }
        public long? IN_TIME_FROM { get; set; }
        public long? IN_TIME_TO { get; set; }
        public long? OUT_TIME_FROM { get; set; }
        public long? OUT_TIME_TO { get; set; }
        public long? OUT_DATE_FROM { get; set; }
        public long? OUT_DATE_TO { get; set; }
        public bool? IS_LOCK_HEIN { get; set; }
        public bool? IS_PAUSE { get; set; }
        public bool? IS_OUT { get; set; }
        public long? PATIENT_ID { get; set; }
        public long? TREATMENT_END_TYPE_ID { get; set; }
        public long? TREATMENT_RESULT_ID { get; set; }
        public long? TDL_TREATMENT_TYPE_ID { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public long? IN_DEPARTMENT_ID { get; set; }
        public long? IN_ROOM_ID { get; set; }
        public bool? IS_CHRONIC { get; set; }
        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }
        public long? TDL_KSK_CONTRACT_ID { get; set; }
        public string IN_CODE__EXACT { get; set; }
        public string OUT_CODE__EXACT { get; set; }
        public string STORE_CODE__EXACT { get; set; }
        public string END_CODE__EXACT { get; set; }
        public string TDL_SOCIAL_INSURANCE_NUMBER__EXACT { get; set; }
        public string PATIENT_NAME { get; set; }
        public long? BRANCH_ID { get; set; }

        public bool? IS_APPROVE_FINISH { get; set; }
        public bool? IS_KSK_APPROVE { get; set; }
        public bool? IS_REQUIRED_APPROVAL { get; set; }
        public bool? IS_RESTRICTED_KSK { get; set; }
        public long? LAST_DEPARTMENT_ID { get; set; }
        public long? HOSPITALIZE_DEPARTMENT_ID { get; set; }

        public long? IN_DATE_EQUAL { get; set; }
        public long? IN_MONTH_EQUAL { get; set; }
        public long? OUT_DATE_EQUAL { get; set; }
        public long? OUT_MONTH_EQUAL { get; set; }
        public long? OUT_YEAR_EQUAL { get; set; }
        public long? IN_YEAR_EQUAL { get; set; }

        public HisTreatmentView4Filter()
            : base()
        {
        }
    }
}
