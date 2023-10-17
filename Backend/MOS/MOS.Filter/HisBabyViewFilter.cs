
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBabyViewFilter : FilterBase
    {
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> BORN_POSITION_IDs { get; set; }
        public List<long> BORN_TYPE_IDs { get; set; }
        public List<long> BORN_RESULT_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }

        public long? TREATMENT_ID { get; set; }
        public long? BORN_POSITION_ID { get; set; }
        public long? BORN_TYPE_ID { get; set; }
        public long? BORN_RESULT_ID { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public string PATIENT_NAME { get; set; }
        public long? BORN_TIME_FROM { get; set; }
        public long? BORN_TIME_TO { get; set; }
        public long? SYNC_RESULT_TYPE { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? ISSUED_DATE_FROM { get; set; }
        public long? ISSUED_DATE_TO { get; set; }

        public HisBabyViewFilter()
            : base()
        {
        }
    }
}
