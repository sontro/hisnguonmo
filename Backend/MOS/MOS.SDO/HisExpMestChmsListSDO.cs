using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestChmsListSDO
    {

        public long WorkingRoomId { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
        public string ReceivingPlace { get; set; }
        public ChmsTypeEnum Type { get; set; }
        public long? ExpMestReasonId { get; set; }

        public List<ExpMestChmsDetailSDO> ExpMests { get; set; }
    }

    public class ExpMestChmsDetailSDO
    {
        public long ImpMediStockId { get; set; }
        public long ExpMediStockId { get; set; }

        public List<ExpMedicineTypeSDO> MedicineTypes { get; set; }
        public List<ExpMaterialTypeSDO> MaterialTypes { get; set; }
        public List<ExpBloodTypeSDO> BloodTypes { get; set; }

        public List<ExpMedicineSDO> Medicines { get; set; }
        public List<ExpMaterialSDO> Materials { get; set; }
    }
}
