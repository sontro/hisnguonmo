using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Patient
{
    public class PatientADO : V_HIS_PATIENT
    {
        public string dobDateTime { get; set; }
        public string cmndDateTime { get; set; }
    }
}
