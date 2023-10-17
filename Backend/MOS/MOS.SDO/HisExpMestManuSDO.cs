using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestManuSDO
    {
        public long? ExpMestId { get; set; }
        public long? ExpMestReasonId { get; set; }
        public long MediStockId { get; set; }
        public long ReqRoomId { get; set; }
        public long ManuImpMestId { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
        public string ReceivingPlace { get; set; }

        public List<ExpMedicineSDO> Medicines { get; set; }
        public List<ExpMaterialSDO> Materials { get; set; }
        public List<ExpBloodSDO> Bloods { get; set; }
    }
}
