using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMedicineReturnAvailableSDO
    {
        public long MediStockId { get; set; }
        public long? MedicineTypeId { get; set; }
        public long? MedicineId { get; set; }
    }
}
