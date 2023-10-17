
namespace MOS.Filter
{
    public class HisEmrCoverConfigFilter : FilterBase
    {
        public long? TREATMENT_TYPE_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }

        public HisEmrCoverConfigFilter()
            : base()
        {
        }
    }
}
