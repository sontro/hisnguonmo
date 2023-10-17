using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
namespace HIS.Desktop.Plugins.ImpUserTemp.ADO
{
    class HisImpTempUserADO : V_HIS_IMP_USER_TEMP_DT
    {
        public HisImpTempUserADO() { }

        public int Action { get; set; }
    }
}
