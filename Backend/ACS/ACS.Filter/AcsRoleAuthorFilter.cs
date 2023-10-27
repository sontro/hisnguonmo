
namespace ACS.Filter
{
    public class AcsRoleAuthorFilter : FilterBase
    {
        public long? ROLE_ID { get; set; }
        public long? AUTHOR_SYSTEM_ID { get; set; }

        public AcsRoleAuthorFilter()
            : base()
        {
        }
    }
}
