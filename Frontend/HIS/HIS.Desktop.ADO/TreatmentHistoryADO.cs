using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class TreatmentHistoryADO
    {
        public long? treatmentId { get; set; }
        public string treatment_code { get; set; }
        public long? patientId { get; set; }
        public string patient_code { get; set; }

    }
}
