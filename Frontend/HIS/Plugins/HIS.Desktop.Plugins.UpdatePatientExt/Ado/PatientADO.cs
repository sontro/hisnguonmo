using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UpdatePatientExt.Ado
{
    public class PatientADO : V_HIS_PATIENT
    {
        public string FATHER_CODE { get; set; }
        public long? FATHER_DOB { get; set; }
        public string MOTHER_CODE { get; set; }
        public long? MOTHER_DOB { get; set; }

    }
}
