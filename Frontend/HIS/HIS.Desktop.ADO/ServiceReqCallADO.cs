using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class ServiceReqCallADO
    {
        public long PatientId { get; set; }
        public ServiceReqCallADO(long patientId)
        {
            this.PatientId = patientId;
        }

    }
}
