using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class MedicineTypeInHospitalSDO
    {
        public List<string> MediStockNames { get; set; }
        public List<string> MediStockCodes { get; set; }
        public List<ExpandoObject> MedicineTypeDatas { get; set; }
    }
}
