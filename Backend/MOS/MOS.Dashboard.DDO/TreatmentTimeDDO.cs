using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.DDO
{
    public class TreatmentTimeDDO
    {
        public long id { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string TreatmentCode { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string GenderName { get; set; }
        public long Dob { get; set; }
        public long? InTime { get; set; }
        public long? OutTime { get; set; }
        public decimal? WaitTime { get; set; }
    }
}
