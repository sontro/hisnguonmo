
namespace MOS.Filter
{
    public class HisDebateEkipUserViewFilter : FilterBase
    {
        public long? DEBATE_ID { get; set; }
        public long? EXECUTE_ROLE_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }

        public HisDebateEkipUserViewFilter()
            : base()
        {
        }
    }
}
