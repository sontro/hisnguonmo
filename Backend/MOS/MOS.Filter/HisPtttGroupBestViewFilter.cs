
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPtttGroupBestViewFilter : FilterBase
    {
        public string PTTT_GROUP_CODE__EXACT { get; set; }
        public string SERVICE_CODE__EXACT { get; set; }
        public string HEIN_SERVICE_BHYT_CODE__EXACT { get; set; }

        public long? PTTT_GROUP_ID { get; set; }
        public long? BED_SERVICE_TYPE_ID { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }

        public List<long> PTTT_GROUP_IDs { get; set; }
        public List<long> BED_SERVICE_TYPE_IDs { get; set; }
        public List<long> SERVICE_TYPE_IDs { get; set; }

        public HisPtttGroupBestViewFilter()
            : base()
        {
        }
    }
}
