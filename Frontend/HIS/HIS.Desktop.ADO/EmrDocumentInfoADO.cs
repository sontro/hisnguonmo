using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class EmrDocumentInfoADO
    {
        public string TreatmentCode { get; set; }
        public string HisCode { get; set; }

        public long TreatmentId { get; set; }
        public long EmrTreatmentId { get; set; }

        public List<long> EmrDocumentTypeIds { get; set; }
    }
}
