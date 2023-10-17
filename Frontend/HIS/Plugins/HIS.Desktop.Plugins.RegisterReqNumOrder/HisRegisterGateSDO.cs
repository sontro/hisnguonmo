using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterReqNumOrder
{
    public class HisRegisterGateSDO : HIS_REGISTER_GATE
    {
        public bool checkStt { get; set; }
        public string DISPLAY_SCREEN { get; set; }
    }
}
