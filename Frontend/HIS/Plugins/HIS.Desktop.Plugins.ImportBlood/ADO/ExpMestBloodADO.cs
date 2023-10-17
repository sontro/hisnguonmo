using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood.ADO
{
    public class ExpMestBloodADO : V_HIS_EXP_MEST_BLOOD
    {
        public int Action { get; set; }
        public long? ID_GRID { get; set; }
    }
}
