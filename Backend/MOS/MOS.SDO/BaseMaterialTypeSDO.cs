using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class BaseMaterialTypeSDO
    {
        public long MaterialTypeId { get; set; }
        public decimal Amount { get; set; }
        public List<long> ExpMestMaterialIds { get; set; }
        public List<long> ExpMestMatyReqIds { get; set; }
    }
}
