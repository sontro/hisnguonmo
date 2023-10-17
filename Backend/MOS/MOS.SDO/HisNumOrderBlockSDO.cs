using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisNumOrderBlockSDO
    {
        public long ROOM_TIME_ID { get; set; }
        public string ROOM_TIME_FROM { get; set; }
        public string ROOM_TIME_TO { get; set; }
        public string ROOM_TIME_NAME { get; set; }
        public long DAY { get; set; }
        public string FROM_TIME { get; set; }
        public string TO_TIME { get; set; }
        public long NUM_ORDER { get; set; }
        public long NUM_ORDER_BLOCK_ID { get; set; }
        public short? IS_ISSUED { get; set; }
    }
}
