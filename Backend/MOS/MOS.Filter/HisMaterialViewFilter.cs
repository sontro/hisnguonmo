
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMaterialViewFilter : FilterBase
    {
        public string BID_NUMBER__EXACT { get; set; }
        public long? BID_ID { get; set; }
        public List<long> BID_IDs { get; set; }

        public string TDL_IMP_MEST_CODE { get; set; }
        public string TDL_IMP_MEST_SUB_CODE { get; set; }

        public HisMaterialViewFilter()
            : base()
        {
        }
    }
}
