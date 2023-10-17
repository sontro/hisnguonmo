using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class MaterialNormationSDO
    {
        public long MaterialTypeId { get; set; }
        public decimal Amount { get; set; }
    }

    public class HisQcNormationSDO
    {
        public long MachineId { get; set; }
        public long QcTypeId { get; set; }
        public List<MaterialNormationSDO> MaterialNormations { get; set; }
    }
}
