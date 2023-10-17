
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentBorrowViewFilter : FilterBase
    {

        public string BORROW_LOGINNAME__EXACT { get; set; }
        public string GIVER_LOGINNAME__EXACT { get; set; }
        public string RECEIVER_LOGINNAME__EXACT { get; set; }
        public string DEPARTMENT_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public string STORE_CODE__EXACT { get; set; }
        public string DATA_STORE_CODE__EXACT { get; set; }

        public long? GIVE_DATE_FROM { get; set; }
        public long? GIVE_DATE_TO { get; set; }
        public long? RECEIVE_DATE_FROM { get; set; }
        public long? RECEIVE_DATE_TO { get; set; }

        public long? TREATMENT_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? PATIENT_ID { get; set; }
        public long? DATA_STORE_ID { get; set; }

        public List<long> TREATMENT_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> PATIENT_IDs { get; set; }
        public List<long> DATA_STORE_IDs { get; set; }

        public bool? IS_RECEIVE { get; set; }

        public HisTreatmentBorrowViewFilter()
            : base()
        {
        }
    }
}
