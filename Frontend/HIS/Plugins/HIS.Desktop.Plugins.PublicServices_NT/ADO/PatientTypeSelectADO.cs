using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PublicServices_NT.ADO
{
    internal class PatientTypeSelectADO
    {
        public PatientTypeSelectADO() { }
        public long ID { get; set; }
        public string PATIENT_TYPE_CODE { get; set; }
        public string PATIENT_TYPE_NAME { get; set; }
    }
}
