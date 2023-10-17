
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisCareDetailViewFilter : FilterBase
    {
        public long? CARE_ID { get; set; }
        public List<long> CARE_IDs { get; set; }

        public HisCareDetailViewFilter()
            : base()
        {
        }
    }
}
