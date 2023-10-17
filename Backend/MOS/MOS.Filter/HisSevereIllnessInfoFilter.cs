
namespace MOS.Filter
{
    public class HisSevereIllnessInfoFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public bool? IS_DEATH { get; set; }

        public HisSevereIllnessInfoFilter()
            : base()
        {
        }
    }
}
