using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestReuseSDO
    {
        public long MediStockId { get; set; }
        public string Description { get; set; }
        public long RequestRoomId { get; set; }
        public List<ImpMestMaterialReusableSDO> MaterialReuseSDOs { get; set; }
    }
}
