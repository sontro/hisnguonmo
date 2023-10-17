using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VaccinationExam.ADO
{
    class ExpMestMedicineADO : V_HIS_EXP_MEST_MEDICINE_5
    {
        public int Action { get; set; }
        public int VaccinationIndex { get; set; }
    }
}
