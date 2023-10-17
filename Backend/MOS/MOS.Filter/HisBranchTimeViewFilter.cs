
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBranchTimeViewFilter : FilterBase
    {
        public long? BRANCH_ID { get; set; }

        public List<long> BRANCH_IDs { get; set; }

        public HisBranchTimeViewFilter()
            : base()
        {
        }
    }
}
