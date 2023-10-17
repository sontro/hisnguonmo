using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisRationSumSDO
    {
        public long? Id { get; set; }
        public long RoomId { get; set; }
        public List<long> ServiceReqIds { get; set; }
    }
}
