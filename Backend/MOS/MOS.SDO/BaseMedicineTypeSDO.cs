using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class BaseMedicineTypeSDO
    {
        public long MedicineTypeId { get; set; }
        public decimal Amount { get; set; }
        public List<long> ExpMestMedicineIds { get; set; }
        public List<long> ExpMestMetyReqIds { get; set; }
    }
}
