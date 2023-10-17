using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    //Loai xuat chuyen kho
    public enum ChmsTypeEnum
    {
        GET, //linh~
        GIVE_BACK //tra
    }

    public class HisExpMestChmsSDO
    {
        public long? ExpMestId { get; set; }
        public long? ExpMestReasonId { get; set; }
        public long MediStockId { get; set; }
        public long ImpMediStockId { get; set; }
        public long ReqRoomId { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
        public string ReceivingPlace { get; set; }

        public ChmsTypeEnum Type { get; set; }
        public List<ExpMedicineTypeSDO> Medicines { get; set; }
        public List<ExpMaterialTypeSDO> Materials { get; set; }
        public List<ExpBloodTypeSDO> Bloods { get; set; }

        public List<ExpMedicineSDO> ExpMedicineSdos { get; set; }
        public List<ExpMaterialSDO> ExpMaterialSdos { get; set; }
    }
}
