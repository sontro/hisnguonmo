using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisSereServView13Filter : FilterBase
    {
        public List<long> PTTT_APPROVAL_STT_IDs { get; set; }
        public List<long> SERVICE_REQ_IDs { get; set; }
        public List<long> HEIN_APPROVAL_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> PTTT_CALENDAR_IDs { get; set; }
        public List<long> EXECUTE_DEPARTMENT_IDs { get; set; }

        public long? PTTT_APPROVAL_STT_ID { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }
        public long? HEIN_APPROVAL_ID { get; set; }
        public long? ID__NOT_EQUAL { get; set; }
        public long? PARENT_ID { get; set; }
        public long? SERVICE_REQ_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public string HEIN_CARD_NUMBER__EXACT { get; set; }
        public string HEIN_CARD_NUMBER { get; set; }
        public bool? IS_SPECIMEN { get; set; }
        public long? INVOICE_ID { get; set; }
        public bool? HAS_INVOICE { get; set; }
        public bool? HAS_EXECUTE { get; set; }
        public bool? IS_EXPEND { get; set; }
        public long? TREATMENT_ID { get; set; }
        public bool? IS_MEDICINE { get; set; }
        public bool? IS_MATERIAL { get; set; }
        public bool? IS_BLOOD { get; set; }
        public bool? IS_IN_CALENDAR { get; set; }
        public long? PTTT_CALENDAR_ID { get; set; }
        public long? EXECUTE_DEPARTMENT_ID { get; set; }
        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }
        public long? REQUEST_DEPARTMENT_ID { get; set; }

        public HisSereServView13Filter()
            : base()
        {

        }
    }
}
