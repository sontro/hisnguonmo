using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.DDO
{
    public class TreatmentTimeAvgDDO
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public decimal MinTime { get; set; }
        public decimal MaxTime { get; set; }
        public decimal AvgTime { get; set; }
    }
}
