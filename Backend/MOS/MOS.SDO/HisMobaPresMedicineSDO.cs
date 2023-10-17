using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMobaPresMedicineSDO
    {
        public decimal Amount { get; set; }
        public long ExpMestMedicineId { get; set; }

        public HisMobaPresMedicineSDO() { }

        public HisMobaPresMedicineSDO(long expMestMedicineId, decimal amount)
        {
            this.ExpMestMedicineId = expMestMedicineId;
            this.Amount = amount;
        }
    }
}
