using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.ADO
{
    public class ProgramADO : MOS.EFMODEL.DataModels.HIS_PROGRAM
    {
        public bool SelectPatient { get; set; }
    }
}
