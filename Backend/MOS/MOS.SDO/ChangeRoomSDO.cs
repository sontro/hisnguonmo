using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ChangeRoomSDO
    {
        public List<long> ServiceReqIds { get; set; }
        public long ExecuteRoomId { get; set; }
        public long RequestRoomId { get; set; }
    }
}
