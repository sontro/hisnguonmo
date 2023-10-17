using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMediStockInventorySDO
    {
        public long MediStockId { get; set; }
        public long WorkingRoomId { get; set; }
        public string Description { get; set; }

        public List<ExpMedicineSDO> ExpMedicines { get; set; }
        public List<ExpMaterialSDO> ExpMaterials { get; set; }

        public List<HisMedicineWithPatySDO> ImpMedicines { get; set; }
        public List<HisMaterialWithPatySDO> ImpMaterials { get; set; }
    }
}
