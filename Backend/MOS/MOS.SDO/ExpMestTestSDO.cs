using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public enum ExpMestTestTypeEnum
    {
        TEST = 1,
        QC = 2
    }

    public class ExpMestTestSDO
    {
        public long MediStockId { get; set; }
        public long RequestRoomId { get; set; }
        public string Description { get; set; }
        public long? MachineId { get; set; }
        public ExpMestTestTypeEnum ExpMestTestType { get; set; }
        public List<ExpMaterialTypeSDO> Materials { get; set; }
        public List<long> SereServIds { get; set; }
    }
}
