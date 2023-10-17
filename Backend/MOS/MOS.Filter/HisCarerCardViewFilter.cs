
namespace MOS.Filter
{
    public class HisCarerCardViewFilter : FilterBase
    {
        public bool? IS_LOST { get; set; }
        public bool? IS_BORROWED { get; set; }

        public HisCarerCardViewFilter()
            : base()
        {
        }
    }
}
