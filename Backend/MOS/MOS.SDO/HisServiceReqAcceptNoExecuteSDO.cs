using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqAcceptNoExecuteSDO
    {
        public long WorkingRoomId { get; set; }
        public long ServiceReqId { get; set; }
        public string NoExecuteReason { get; set; }
    }
}
