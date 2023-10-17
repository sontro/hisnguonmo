
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMediRecordBorrowViewFilter : FilterBase
    {

        public string BORROW_LOGINNAME__EXACT { get; set; }
        public string GIVER_LOGINNAME__EXACT { get; set; }
        public string RECEIVER_LOGINNAME__EXACT { get; set; }

        public long? GIVE_DATE__EQUAL { get; set; }
        public long? RECEIVE_DATE__EQUAL { get; set; }
        public long? APPOINTMENT_DATE__EQUAL { get; set; }

        public long? GIVE_MONTH__EQUAL { get; set; }
        public long? RECEIVE_MONTH__EQUAL { get; set; }
        public long? APPOINTMENT_MONTH__EQUAL { get; set; }

        public long? MEDI_RECORD_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? DATA_STORE_ID { get; set; }

        public List<long> MEDI_RECORD_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> DATA_STORE_IDs { get; set; }

        public bool? IS_RECEIVE { get; set; }

        public string PATIENT_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string STORE_CODE__EXACT { get; set; }

        public HisMediRecordBorrowViewFilter()
            : base()
        {
        }
    }
}
