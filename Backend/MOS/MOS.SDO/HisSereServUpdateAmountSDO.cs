using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class UpdateBedAmountSDO
    {
        public long SereServId { get; set; }
        public long WorkingRoomId { get; set; }
        public decimal Amount { get; set; }
    }
}
