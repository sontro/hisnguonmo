
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPermissionFilter : FilterBase
    {
        public long? PERMISSION_TYPE_ID { get; set; }
        public List<long> PERMISSION_TYPE_IDs { get; set; }

        public HisPermissionFilter()
            : base()
        {
        }
    }
}
