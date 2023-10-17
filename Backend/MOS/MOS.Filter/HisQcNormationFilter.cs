
namespace MOS.Filter
{
    public class HisQcNormationFilter : FilterBase
    {
        public long? QC_TYPE_ID { get; set; }
        public long? MACHINE_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }

        public HisQcNormationFilter()
            : base()
        {
        }
    }
}
