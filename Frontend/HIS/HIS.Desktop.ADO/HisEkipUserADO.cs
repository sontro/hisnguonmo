using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class HisEkipUserADO : MOS.EFMODEL.DataModels.V_HIS_EKIP_USER
    {
        public int Action { get; set; }
        public bool IsMinus { get; set; }
        public bool IsPlus { get; set; }
    }
}
