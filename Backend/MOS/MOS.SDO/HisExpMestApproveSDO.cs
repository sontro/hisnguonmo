using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestApproveSDO : HisExpMestSDO
    {
        public List<ExpMedicineTypeSDO> Medicines { get; set; }
        public List<ExpMaterialTypeSDO> Materials { get; set; }
        public List<ExpBloodSDO> Bloods { get; set; }
        public List<PresMaterialBySerialNumberSDO> SerialNumbers { get; set; }
        public List<HisTestResultTDO> TestResults { get; set; }
        public bool? IsFinish { get; set; } //chi truyen len voi kho tu dong thuc xuat
        public string Description { get; set; }
    }
}
