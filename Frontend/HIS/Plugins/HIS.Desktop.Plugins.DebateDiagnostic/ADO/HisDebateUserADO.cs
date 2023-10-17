using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DebateDiagnostic.ADO
{
    class HisDebateUserADO : MOS.EFMODEL.DataModels.HIS_DEBATE_USER
    {
        public int Action { get; set; }
        public bool PRESIDENT { get; set; }
        public bool SECRETARY { get; set; }
    }
}
