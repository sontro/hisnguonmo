
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceNumOrderViewFilter : FilterBase
    {

        public long? SERVICE_ID { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }

        public List<long> SERVICE_IDs { get; set; }
        public List<long> REQUEST_ROOM_IDs { get; set; }


        public long? ID__NOT_EQUAL { get; set; }

        public HisServiceNumOrderViewFilter()
            : base()
        {
        }
    }
}
