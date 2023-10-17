
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMediRecordView2Filter : FilterBase
    {
        public long? DATA_STORE_ID { get; set; }
        public long? STORE_TIME_FROM { get; set; }
        public long? STORE_TIME_TO { get; set; }
        public long? PROGRAM_ID { get; set; }
        public long? MEDI_RECORD_TYPE_ID { get; set; }

        public string STORE_CODE__EXACT { get; set; }
        public string TREATMENT_CODE { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }

        public List<long> DATA_STORE_IDs { get; set; }
        public List<long> PROGRAM_IDs { get; set; }
        public List<long> MEDI_RECORD_TYPE_IDs { get; set; }

        public bool? IS_NOT_STORED { get; set; }

        public HisMediRecordView2Filter()
            : base()
        {
        }
    }
}
