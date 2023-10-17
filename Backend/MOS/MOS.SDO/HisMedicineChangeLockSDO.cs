using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMedicineChangeLockSDO
    {
        public long MedicineId { get; set; }
        public long? MediStockId { get; set; }
        public long? WorkingRoomId { get; set;}
        public string LockingReason { get; set; }
    }
}
