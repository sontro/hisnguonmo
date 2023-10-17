
namespace MOS.Filter
{
    public class HisQcNormationViewFilter : FilterBase
    {
        public long? QC_TYPE_ID { get; set; }
        public long? MACHINE_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }

        public HisQcNormationViewFilter()
            : base()
        {
        }
    }
}
