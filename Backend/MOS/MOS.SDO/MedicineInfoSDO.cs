using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class MedicineInfoSDO
    {
        public long IntructionTime { get; set; }
        public string OverResultTestReason { get; set; }
        public string OverKidneyReason { get; set; }
        public bool IsNoPrescription { get; set; }
    }
}
