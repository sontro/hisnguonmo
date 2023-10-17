using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class OddMedicineTypeSDO
    {
        public long MedicineTypeId { get; set; }
        public decimal Amount { get; set; }

        public OddMedicineTypeSDO()
        {

        }

        public OddMedicineTypeSDO(long medicineTypeId, decimal amount)
        {
            this.MedicineTypeId = medicineTypeId;
            this.Amount = amount;
        }
    }
}
