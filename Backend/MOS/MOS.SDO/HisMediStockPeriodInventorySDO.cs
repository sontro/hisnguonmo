using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMestPeriodMediSDO
    {
        public long Id { get; set; }
        public decimal InventoryAmount { get; set; }
    }

    public class HisMestPeriodMateSDO
    {
        public long Id { get; set; }
        public decimal InventoryAmount { get; set; }
    }

    public class HisMediStockPeriodInventorySDO
    {
        public long MediStockPeriodId { get; set; }
        public List<HisMestPeriodMateSDO> Materials { get; set; }
        public List<HisMestPeriodMediSDO> Medicines { get; set; }
    }
}
