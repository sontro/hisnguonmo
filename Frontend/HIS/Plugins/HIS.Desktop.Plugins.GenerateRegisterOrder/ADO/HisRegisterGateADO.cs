using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.GenerateRegisterOrder.ADO
{
    public class HisRegisterGateADO : HIS_REGISTER_GATE
    {
        public bool IsUpdate { get; set; }

        public long BEGIN_NUM_ORDER { get; set; }
        public long CURRENT_NUM_ORDER { get; set; }
        public bool isChecked { get; set; }
        public HisRegisterGateADO()
        {

        }
    }
}
