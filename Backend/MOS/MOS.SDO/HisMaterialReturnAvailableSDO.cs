using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMaterialReturnAvailableSDO
    {
        public long MediStockId { get; set; }
        public long? MaterialTypeId { get; set; }
        public long? MaterialId { get; set; }
    }
}
