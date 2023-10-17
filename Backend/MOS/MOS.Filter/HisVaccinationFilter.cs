
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisVaccinationFilter : FilterBase
    {
        public string VACCINATION_CODE__EXACT { get; set; }
        public string REQUEST_LOGINNAME__EXACT { get; set; }
        public string EXECUTE_LOGINNAME__EXACT { get; set; }
        public string FOLLOW_LOGINNAME__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        
        public long? PATIENT_ID { get; set; }
        public long? BRANCH_ID { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }
        public long? REQUEST_DEPARTMENT_ID { get; set; }        
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? EXECUTE_DEPARTMENT_ID { get; set; }
        public long? TDL_PATIENT_GENDER_ID { get; set; }
        public long? VACCINATION_REACT_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public long? VACCINATION_EXAM_ID { get; set; }
        public long? BILL_ID { get; set; }

        public List<long> PATIENT_IDs { get; set; }
        public List<long> BRANCH_IDs { get; set; }
        public List<long> REQUEST_ROOM_IDs { get; set; }
        public List<long> REQUEST_DEPARTMENT_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> EXECUTE_DEPARTMENT_IDs { get; set; }
        public List<long> TDL_PATIENT_GENDER_IDs { get; set; }
        public List<long> VACCINATION_REACT_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }
        public List<long> VACCINATION_EXAM_IDs { get; set; }
        public List<long> BILL_IDs { get; set; }

        public long? REQUEST_TIME_FROM { get; set; }
        public long? REQUEST_DATE_FROM { get; set; }
        public long? EXECUTE_TIME_FROM { get; set; }
        public long? EXECUTE_DATE_FROM { get; set; }
        public long? REACT_TIME_FROM { get; set; }
        public long? DEATH_TIME_FROM { get; set; }

        public long? REQUEST_TIME_TO { get; set; }
        public long? REQUEST_DATE_TO { get; set; }
        public long? EXECUTE_TIME_TO { get; set; }
        public long? EXECUTE_DATE_TO { get; set; }
        public long? REACT_TIME_TO { get; set; }
        public long? DEATH_TIME_TO { get; set; }

        public bool? HAS_EXECUTE_TIME { get; set; }

        public HisVaccinationFilter()
            : base()
        {
        }
    }
}
