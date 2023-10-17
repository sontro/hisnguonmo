
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentView1Filter : FilterBase
    {
        public List<long> END_ROOM_IDs { get; set; }
        public List<long> DATA_STORE_ID_NULL__OR__INs { get; set; }
        public List<long> BRANCH_IDs { get; set; }
        public List<long> TDL_PATIENT_TYPE_IDs { get; set; }
        public List<long> TDL_TREATMENT_TYPE_IDs { get; set; }
        public List<long> APPROVAL_STORE_STT_IDs { get; set; }

        public List<string> TDL_HEIN_CARD_NUMBER_PREFIXs { get; set; }
        public List<string> TDL_HEIN_CARD_NUMBER_PREFIX__NOT_INs { get; set; }

        public List<long> XML4210_RESULTs { get; set; }
        public List<long> COLLINEAR_XML4210_RESULTs { get; set; }

        public string KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME { get; set; }
        public long? FEE_LOCK_TIME_FROM { get; set; }
        public long? FEE_LOCK_TIME_TO { get; set; }
        public long? DOB_FROM { get; set; }
        public long? DOB_TO { get; set; }
        public long? IN_TIME_FROM { get; set; }
        public long? IN_TIME_TO { get; set; }
        public long? OUT_TIME_FROM { get; set; }
        public long? OUT_TIME_TO { get; set; }
        public bool? IS_LOCK_HEIN { get; set; }
        public bool? IS_PAUSE { get; set; }
        public bool? IS_OUT { get; set; }
        public long? PATIENT_ID { get; set; }
        public long? TREATMENT_END_TYPE_ID { get; set; }
        public bool? HAS_PATY_ALTER_BHYT { get; set; }
        public bool? HAS_HEIN_APPROVAL { get; set; }
        public bool? HAS_HEIN_APPROVAL_OR_IS_PAUSE { get; set; }
        public bool? HAS_NO_XML_URL_HEIN_APPROVAL { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public long? BRANCH_ID { get; set; }
        public string TDL_HEIN_CARD_NUMBER__EXACT { get; set; }
        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public long? TDL_TREATMENT_TYPE_ID { get; set; }
        public long? APPROVAL_STORE_STT_ID { get; set; }

        public long? XML4210_RESULT { get; set; }
        public long? COLLINEAR_XML4210_RESULT { get; set; }

        public bool? IS_APPROVE_FINISH { get; set; }
        public bool? HAS_IN_CODE { get; set; }
        public long? REQUEST_HOSPITALIZE_TIME_FROM { get; set; }
        public long? REQUEST_HOSPITALIZE_TIME_TO { get; set; }

        public long? HEIN_LOCK_TIME_FROM { get; set; }
        public long? HEIN_LOCK_TIME_TO { get; set; }
        public List<string> ICD_CODEs { get; set; }

        public short? XML130_RESULT { get; set; }
        public bool? HAS_XML130_RESULT { get; set; }

        public HisTreatmentView1Filter()
            : base()
        {
        }
    }
}
