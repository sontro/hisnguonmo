using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisMediStockReplaceSDOFilter
    {
        public long MediStockId { get; set; }
        public List<long> MedicineTypeIds { get; set; }
        public List<long> MaterialTypeIds { get; set; }
    }
}
