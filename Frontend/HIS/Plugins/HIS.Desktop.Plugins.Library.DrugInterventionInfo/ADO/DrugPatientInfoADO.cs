using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    class DrugPatientInfoADO
    {
        public PatientInfoADO info { get; set; }
        public List<DrugInfoADO> drugList { get; set; }
    }
}
