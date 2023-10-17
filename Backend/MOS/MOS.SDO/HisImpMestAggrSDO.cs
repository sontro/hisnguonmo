using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestAggrSDO
    {
        public List<long> ImpMestIds { get; set; }
        public long RequestRoomId { get; set; }
        public long? OddMediStockId { get; set; }//kho chua thuoc le
        public List<OddMedicineTypeSDO> OddMedicineTypes { get; set; }//Danh sach thuoc le
        public List<OddMaterialTypeSDO> OddMaterialTypes { get; set; }//Danh sach vat tu le
    }
}
