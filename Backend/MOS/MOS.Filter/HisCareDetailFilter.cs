
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisCareDetailFilter : FilterBase
    {
        public long? CARE_ID { get; set; }
        public long? CARE_TYPE_ID { get; set; }
        public List<long> CARE_IDs { get; set; }

        public HisCareDetailFilter()
            : base()
        {
        }
    }
}
