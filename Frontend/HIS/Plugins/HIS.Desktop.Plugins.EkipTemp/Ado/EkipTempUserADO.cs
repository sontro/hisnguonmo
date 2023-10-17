using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EkipTemp.Ado
{
    class EkipTempUserADO : V_HIS_EKIP_TEMP_USER
    {
        public EkipTempUserADO() { }

        public int Action { get; set; }
    }
}
