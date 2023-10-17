
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisStationFilter : FilterBase
    {
        public string STATION_CODE__EXACT { get; set; }
        public long? ROOM_ID { get; set; }

        public List<long> ROOM_IDs { get; set; }

        public HisStationFilter()
            : base()
        {
        }
    }
}
