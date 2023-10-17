
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPtttGroupBestFilter : FilterBase
    {
        public long? PTTT_GROUP_ID { get; set; }
        public long? BED_SERVICE_TYPE_ID { get; set; }

        public List<long> PTTT_GROUP_IDs { get; set; }
        public List<long> BED_SERVICE_TYPE_IDs { get; set; }

        public HisPtttGroupBestFilter()
            : base()
        {
        }
    }
}
