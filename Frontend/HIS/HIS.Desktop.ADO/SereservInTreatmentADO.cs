using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class SereservInTreatmentADO
    {
        public long TreatmentId { get; set; }
        public long IntructionTime { get; set; }
        public long ServiceReqId { get; set; }

        public SereservInTreatmentADO(long _treatmentId, long _intructionTime, long _serviceReqId)
        {
            this.TreatmentId = _treatmentId;
            this.IntructionTime = _intructionTime;
            this.ServiceReqId = _serviceReqId;
        }
    }
}
