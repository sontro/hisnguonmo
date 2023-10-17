
namespace MOS.Filter
{
    public class HisDebateUserFilter : FilterBase
    {
        public long? DEBATE_ID { get; set; }
        public long? DEBATE_TEMP_ID { get; set; }

        public HisDebateUserFilter()
            : base()
        {
        }
    }
}
