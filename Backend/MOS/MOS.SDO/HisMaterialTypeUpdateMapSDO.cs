using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMaterialTypeUpdateMapSDO
    {
        public long MaterialTypeId { get; set; }
        public List<long> MapMaterialTypeIds { get; set; }
    }
}
