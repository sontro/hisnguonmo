using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisSereServExtIsFeeSDO
    {
        public long WorkingRoomId { get; set; }
        public long SereServId { get; set; }
        public bool IsFee { get; set; }
        public bool IsGatherData { get; set; }
    }
}
