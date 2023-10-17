using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMobaPresMaterialSDO
    {
        public long ExpMestMaterialId { get; set; }
        public decimal Amount { get; set; }

        public HisMobaPresMaterialSDO() { }

        public HisMobaPresMaterialSDO(long expMestMaterialId, decimal amount)
        {
            this.ExpMestMaterialId = expMestMaterialId;
            this.Amount = amount;
        }
    }
}
