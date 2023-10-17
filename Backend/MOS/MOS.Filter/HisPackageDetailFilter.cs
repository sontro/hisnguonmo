
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPackageDetailFilter : FilterBase
    {
        public List<long> PACKAGE_IDs { get; set; }
        public List<long> SERVICE_IDs { get; set; }

        public long? PACKAGE_ID { get; set; }
        public long? SERVICE_ID { get; set; }

        public HisPackageDetailFilter()
            : base()
        {
        }
    }
}
