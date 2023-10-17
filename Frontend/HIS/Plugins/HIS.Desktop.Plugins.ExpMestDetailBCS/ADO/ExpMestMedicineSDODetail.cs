using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestDetailBCS.ADO
{
    public class ExpMestMedicineSDODetail : V_HIS_EXP_MEST_MEDICINE_1
    {
        public string REPLACE_FOR_NAME { get; set; }
        public string packageNumbers { get; set; }
        public string expiredDates { get; set; }

    }
}
