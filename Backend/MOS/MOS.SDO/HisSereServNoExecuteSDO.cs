using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisSereServNoExecuteSDO
    {
        public long TreatmentId { get; set; }
        public List<long> ServiceReqIds { get; set; }
        public bool IsNoExecute { get; set; }
        public long RequestRoomId { get; set; }
    }
}
