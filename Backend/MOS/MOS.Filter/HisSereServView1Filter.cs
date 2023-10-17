using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisSereServView1Filter : FilterBase
    {
        public List<long> SERVICE_REQ_IDs { get; set; }
        public List<long> SERVICE_TYPE_IDs { get; set; }
        public List<long> NOT_IN_SERVICE_TYPE_IDs { get; set; }
        public List<long> SERVICE_REQ_STT_IDs { get; set; }
        public List<long> NOT_IN_SERVICE_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> RATION_SUM_IDs { get; set; }
        public List<long> RATION_TIME_IDs { get; set; }
        public List<long> SERVICE_REQ_PARENT_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public long? TREATMENT_ID { get; set; }
        public long? TREATMENT_ID__NOT_EQUAL { get; set; }
        public long? TDL_PATIENT_ID { get; set; }
        public long? ID__NOT_EQUAL { get; set; }
        public long? PARENT_ID { get; set; }
        public long? SERVICE_REQ_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public string HEIN_CARD_NUMBER__EXACT { get; set; }
        public string HEIN_CARD_NUMBER { get; set; }
        public bool? IS_SPECIMEN { get; set; }
        public bool? IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY { get; set; }

        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }
        public long? PLAN_TIME_FROM_FROM { get; set; }
        public long? PLAN_TIME_FROM_TO { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }
        public long? INVOICE_ID { get; set; }
        public bool? HAS_INVOICE { get; set; }
        public bool? IS_EXPEND { get; set; }
        public bool? HAS_EXECUTE { get; set; }
        public long? RATION_SUM_ID { get; set; }
        public long? RATION_TIME_ID { get; set; }
        public long? SERVICE_REQ_PARENT_ID { get; set; }
        
        public HisSereServView1Filter()
            : base()
        {

        }
    }
}
