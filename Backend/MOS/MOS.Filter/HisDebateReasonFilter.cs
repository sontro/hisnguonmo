
namespace MOS.Filter
{
    public class HisDebateReasonFilter : FilterBase
    {
        public string DEBATE_REASON_CODE__EXACT { get; set; }
        public string DEBATE_REASON_NAME__EXACT { get; set; }

        public HisDebateReasonFilter()
            : base()
        {
        }
    }
}
