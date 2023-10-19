
using System.Collections.Generic;
namespace ACS.Filter
{
    public class AcsModuleRoleViewFilter : FilterBase
    {
        public long? APPLICATION_ID { get; set; }
        public string APPLICATION_CODE { get; set; }
        public long? ROLE_ID { get; set; }
        public long? MODULE_ID { get; set; }
        public List<long> ROLE_IDs { get; set; }
        public AcsModuleRoleViewFilter()
            : base()
        {
        }
    }
}
