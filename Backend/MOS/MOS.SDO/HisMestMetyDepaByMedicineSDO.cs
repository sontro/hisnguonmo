using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMestMetyDepaByMedicineSDO
    {
        public long MedicineTypeId { get; set; }
        public List<long> DepartmentIds { get; set; }
    }
}
