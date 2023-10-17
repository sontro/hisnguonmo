using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisTestUpdateBarcodeTDO
    {
        public long? TimeFrom { get; set; }
        public long? TimeTo { get; set; }
        public string PatientCode { get; set; }
        public string TreatmentCode { get; set; }
        public string TurnCode { get; set; }
        public string ServiceReqCode { get; set; }
        public string Barcode { get; set; }
    }
}
