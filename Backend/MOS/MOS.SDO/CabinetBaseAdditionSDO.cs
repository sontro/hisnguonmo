using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class CabinetBaseAdditionSDO
    {
        public long? Id { get; set; }
        public long CabinetMediStockId { get; set; }
        public long ExpMestMediStockId { get; set; }
        public long WorkingRoomId { get; set; }
        public string Description { get; set; }

        public List<ExpMaterialTypeSDO> MaterialTypes { get; set; }
        public List<ExpMedicineTypeSDO> MedicineTypes { get; set; }
    }
}
