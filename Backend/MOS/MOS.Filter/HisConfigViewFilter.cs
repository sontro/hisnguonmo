
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisConfigViewFilter : FilterBase
    {
        public long? BRANCH_ID { get; set; }
        public long? NULL__OR__BRANCH_ID { get; set; }
        public List<long> BRANCH_IDs { get; set; }

        public string BRANCH_CODE__EXACT { get; set; }


        public HisConfigViewFilter()
            : base()
        {
        }
    }
}
