using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.Roche
{
    public class RocheAstmOrderData
    {
        public string OrderCode { get; set; }
        public List<string> TestIndexCodes { get; set; }
        public DateTime OrderDate { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
