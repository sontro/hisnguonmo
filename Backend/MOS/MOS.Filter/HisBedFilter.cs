
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBedFilter : FilterBase
    {
        public long? BED_TYPE_ID { get; set; }
        public List<long> BED_TYPE_IDs { get; set; }

        public long? BED_ROOM_ID { get; set; }
        public List<long> BED_ROOM_IDs { get; set; }

        public string BED_NAME__EXACT { get; set; }

        public long? ID__NOT_EQUAL { get; set; }

        public HisBedFilter()
            : base()
        {
        }
    }
}
