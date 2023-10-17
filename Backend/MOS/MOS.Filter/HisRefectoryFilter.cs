
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRefectoryFilter : FilterBase
    {
        public string REFECTORY_CODE__EXACT { get; set; }

        public long? ROOM_ID { get; set; }

        public List<long> ROOM_IDs { get; set; }

        public HisRefectoryFilter()
            : base()
        {
        }
    }
}
