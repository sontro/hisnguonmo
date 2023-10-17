using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientVer7
{
    public class VaccinationSttSDO : MOS.EFMODEL.DataModels.HIS_VACCINATION_STT
    {
        public bool checkStt { get; set; }
        public string NAME_STATUS { get; set; }
    }
}
