using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class CabinetBaseCompensationSDO
    {
        public long? MediStockId { get; set; }
        public long CabinetMediStockId { get; set; }
        public long WorkingRoomId { get; set; }
        public string Description { get; set; }
        public List<BaseMedicineTypeSDO> MedicineTypes { get; set; }
        public List<BaseMaterialTypeSDO> MaterialTypes { get; set; }
        public long? ExpMestReasonId { get; set; }
    }
}
