
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMediRecordBorrowFilter : FilterBase
    {

        public string BORROW_LOGINNAME__EXACT { get; set; }
        public string GIVER_LOGINNAME__EXACT { get; set; }
        public string RECEIVER_LOGINNAME__EXACT { get; set; }

        public long? GIVE_DATE_FROM { get; set; }
        public long? GIVE_DATE_TO { get; set; }
        public long? RECEIVE_DATE_FROM { get; set; }
        public long? RECEIVE_DATE_TO { get; set; }

        public long? MEDI_RECORD_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }

        public List<long> MEDI_RECORD_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }

        public bool? IS_RECEIVE { get; set; }

        public HisMediRecordBorrowFilter()
            : base()
        {
        }
    }
}
