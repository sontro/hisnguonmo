
namespace MOS.Filter
{
    public class HisKskOtherFilter : FilterBase
    {
        public long? SERVICE_REQ_ID { get; set; }
        public long? TDL_PATIENT_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }

        public HisKskOtherFilter()
            : base()
        {
        }
    }
}
