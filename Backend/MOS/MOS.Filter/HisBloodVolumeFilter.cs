
namespace MOS.Filter
{
    public class HisBloodVolumeFilter : FilterBase
    {
        public long? ID__NOT_EQUAL { get; set; }
        public decimal? VOLUMN__EQUAL { get; set; }

        public HisBloodVolumeFilter()
            : base()
        {
        }
    }
}
