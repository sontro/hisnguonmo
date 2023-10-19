
using System.Collections.Generic;
namespace ACS.Filter
{
    public class AcsControlRoleViewFilter : FilterBase
    {
        public long? ROLE_ID { get; set; }
        public long? CONTROL_ID { get; set; }
        public List<long> ROLE_IDs { get; set; }

        public AcsControlRoleViewFilter()
            : base()
        {
        }
    }
}
