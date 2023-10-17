using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD130.ADO
{
    public class ConfigSyncADO
    {
        public List<long> branchIds { get; set; }
        public List<long> patientTypeIds { get; set; }
        public List<long> treatmentTypeIds { get; set; }
        public int? statusId { get; set; }
        public decimal period { get; set; }
    }
}
