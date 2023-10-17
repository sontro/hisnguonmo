
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisKskServiceFilter : FilterBase
    {
        public long? KSK_ID { get; set; }
        public long? SERVICE_ID { get; set; }
        public long? ROOM_ID { get; set; }

        public List<long> KSK_IDs { get; set; }
        public List<long> SERVICE_IDs { get; set; }
        public List<long> ROOM_IDs { get; set; }

        public HisKskServiceFilter()
            : base()
        {
        }
    }
}
