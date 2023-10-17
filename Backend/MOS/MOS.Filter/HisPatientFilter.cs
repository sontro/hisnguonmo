
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPatientFilter : FilterBase
    {
        public class HeinCardNumberOrId
        {
            public List<string> HeinCardNumbers { get; set; }
            public List<long> Ids { get; set; }
        }

        public HeinCardNumberOrId HEIN_CARD_NUMBER__OR__IDs { get; set; }

        public string PATIENT_CODE { get; set; }
        public string PATIENT_NAME { get; set; }
        public long? GENDER_ID { get; set; }
        public long? DOB { get; set; }
        public long? CAREER_ID { get; set; }
        public long? MILITARY_RANK_ID { get; set; }
        public long? WORK_PLACE_ID { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public string PATIENT_STORE_CODE__EXACT { get; set; }
        public long? ID__NOT_EQUAL { get; set; }
        public string HRM_EMPLOYEE_CODE__EXACT { get; set; }
        public long? BRANCH_ID { get; set; }
        public long? PATIENT_CLASSIFY_ID { get; set; }
        public List<long> PATIENT_CLASSIFY_IDs { get; set; }
        public string REGISTER_CODE__EXACT { get; set; }

        public HisPatientFilter()
            : base()
        {

        }
    }
}
