
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentFeeView2Filter : FilterBase
    {
        public bool? IS_PAUSE { get; set; }
        public decimal? TOTAL_HEIN_PRICE__GREATER_THAN { get; set; }
        public long? PATIENT_ID { get; set; }
        public short? IS_LOCK_HEIN { get; set; }
        public string VIR_PATIENT_NAME { get; set; }
        public string PATIENT_CODE { get; set; }
        public string TREATMENT_CODE { get; set; }

        public long? TDL_TREATMENT_TYPE_ID { get; set; }
        public long? END_DEPARTMENT_ID { get; set; }

        public List<long> TDL_PATIENT_TYPE_IDs { get; set; }
        public List<long> PATIENT_IDs { get; set; }
        public List<long> TDL_TREATMENT_TYPE_IDs { get; set; }
        public List<long> END_DEPARTMENT_IDs { get; set; }
        public List<long> BRANCH_IDs { get; set; }

        public long? BRANCH_ID { get; set; }
        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }
        public long? OUT_TIME_FROM { get; set; }
        public long? OUT_TIME_TO { get; set; }
        public long? OUT_DATE_FROM { get; set; }
        public long? OUT_DATE_TO { get; set; }
        
        public string TREATMENT_CODE__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public string KEY_WORD__VIR_PATIENT_NAME__PATIENT_CODE__TREATMENT_CODE__GENDER_NAME { get; set; }

        public long? FUND_ID { get; set; }
        public List<long> FUND_IDs { get; set; }
        public bool? HAS_FUND_ID { get; set; }
        public bool? HAS_FUND_PAY_TIME { get; set; }
        public long? FUND_PAY_TIME_FROM { get; set; }
        public long? FUND_PAY_TIME_TO { get; set; }
        public string FUND_CODE__EXACT { get; set; }

        public HisTreatmentFeeView2Filter()
            : base()
        {
        }
    }
}
