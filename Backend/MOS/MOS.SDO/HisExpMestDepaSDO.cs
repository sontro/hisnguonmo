using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestDepaSDO
    {
        public long? ExpMestId { get; set; }
        public long? ExpMestReasonId { get; set; }
        public long MediStockId { get; set; }
        public long ReqRoomId { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }
        public string ReceivingPlace { get; set; }
        public long? RemedyCount { get; set; }

        public List<ExpMedicineTypeSDO> Medicines { get; set; }
        public List<ExpMaterialTypeSDO> Materials { get; set; }
        public List<ExpBloodTypeSDO> Bloods { get; set; }
    }
}
