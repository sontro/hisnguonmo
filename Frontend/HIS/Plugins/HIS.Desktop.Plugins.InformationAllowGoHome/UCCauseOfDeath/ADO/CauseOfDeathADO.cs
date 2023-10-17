using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.InformationAllowGoHome.UCCauseOfDeath.ADO
{
    public class CauseOfDeathADO
    {
        public CauseOfDeathADO() { }
        public HIS_TREATMENT Treatment { get; set; }
        public HIS_SEVERE_ILLNESS_INFO SevereIllNessInfo { get; set; }
        public List<HIS_EVENTS_CAUSES_DEATH> ListEventsCausesDeath { get; set; }
    }
}
