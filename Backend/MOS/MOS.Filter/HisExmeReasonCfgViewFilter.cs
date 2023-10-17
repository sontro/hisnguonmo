
namespace MOS.Filter
{
    public class HisExmeReasonCfgViewFilter : FilterBase
    {
        public long? EXP_MEST_REASON_ID { get; set; }
        public long? PATIENT_CLASSIFY_ID { get; set; }
        public long? TREATMENT_TYPE_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public long? ID__NOT_EQUAL { get; set; }

        public HisExmeReasonCfgViewFilter()
            : base()
        {
        }
    }
}
