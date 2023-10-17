
namespace MOS.Filter
{
    public class HisCarerCardFilter : FilterBase
    {
        public bool? IS_LOST { get; set; }
        public bool? IS_BORROWED { get; set; }

        public string CARER_CARD_NUMBER__EXACT { get; set; }

        public HisCarerCardFilter()
            : base()
        {
        }
    }
}
