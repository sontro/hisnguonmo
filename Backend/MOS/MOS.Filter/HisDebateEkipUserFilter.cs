
namespace MOS.Filter
{
    public class HisDebateEkipUserFilter : FilterBase
    {
        public long? DEBATE_ID { get; set; }
        public long? EXECUTE_ROLE_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }

        public HisDebateEkipUserFilter()
            : base()
        {
        }
    }
}
