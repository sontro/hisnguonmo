using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class PatientADO
    {
        public long PatientId { get; set; }

        public PatientADO(long patientId)
        {
            this.PatientId = patientId;
        }
    }
}
