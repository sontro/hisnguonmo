using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class MobaMaterialSDO
    {
        public long MaterialId { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }

    public class MobaMedicineSDO
    {
        public long MedicineId { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }

    public class ImpMestAggrApprovalSDO
    {
        public long ImpMestId { get; set; }
        public long RequestRoomId { get; set; }
        //Kho de luu thuoc/vat tu bi tu choi nhap
        public long? RejectedMediStockId { get; set; }
        public List<MobaMaterialSDO> ApprovalMaterials { get; set; }
        public List<MobaMedicineSDO> ApprovalMedicines { get; set; }
    }
}
