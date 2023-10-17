using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMobaMatyReqSDO
    {
        public long ExpMestMatyReqId { get; set; }
        public List<HisMobaMaterialSDO> HisMobaMaterials { get; set; }
    }
}
