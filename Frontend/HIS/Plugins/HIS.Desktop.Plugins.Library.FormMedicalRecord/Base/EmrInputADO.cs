using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.FormMedicalRecord.Base
{
    public class EmrInputADO
    {
        public long TreatmentId { get; set; }
        public long PatientId { get; set; }
        public long? EmrCoverTypeId { get; set; }
        public List<long> lstEmrCoverTypeId { get; set; }
        public long? roomId { get; set; }
    }
}
