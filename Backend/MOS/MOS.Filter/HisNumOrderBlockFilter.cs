
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisNumOrderBlockFilter : FilterBase
    {
        public long? ROOM_TIME_ID { get; set; }
        public List<long> ROOM_TIME_IDs { get; set; }
        public long? NUM_ORDER { get; set; }
        public long? ID__NOT_EQUAL { get; set; }

        public HisNumOrderBlockFilter()
            : base()
        {
        }
    }
}
