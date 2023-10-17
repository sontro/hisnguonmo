using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisHoldReturnUpdateSDO
    {
        public long Id { get; set; }
        public long WorkingRoomId { get; set; }
        public long TreatmentId { get; set; }
        public long HoldTime { get; set; }
        public string HeinCardNumber { get; set; }
        public List<long> DocHoldTypeIds { get; set; }
    }
}
