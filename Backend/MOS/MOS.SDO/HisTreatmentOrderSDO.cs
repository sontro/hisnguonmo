using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentOrderSDO
    {
        public long? Id { get; set; }
        public long InDate { get; set; }
        public long TreatmentOrder { get; set; }
        public string TreatmentCode { get; set; }
    }
}
