
namespace MOS.Filter
{
    public class HisDebateTempFilter : FilterBase
    {
        public string DEBATE_CODE__EXACT { get; set; }

        public string KEY_WORD__CODE_OR_NAME { get; set; }

        public HisDebateTempFilter()
            : base()
        {
        }
    }
}
