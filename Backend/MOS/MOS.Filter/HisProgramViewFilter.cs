
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisProgramViewFilter : FilterBase
    {
        public List<long> DATA_STORE_IDs { get; set; }
        public List<long> BRANCH_IDs { get; set; }

        public long? DATA_STORE_ID { get; set; }
        public long? BRANCH_ID { get; set; }

        public HisProgramViewFilter()
            : base()
        {
        }
    }
}
