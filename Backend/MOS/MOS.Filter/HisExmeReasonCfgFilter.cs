
namespace MOS.Filter
{
    public class HisExmeReasonCfgFilter : FilterBase
    {
        public long? EXP_MEST_REASON_ID { get; set; }
        public long? PATIENT_CLASSIFY_ID { get; set; }
        public long? TREATMENT_TYPE_ID { get; set; }

        public HisExmeReasonCfgFilter()
            : base()
        {
        }
    }
}
