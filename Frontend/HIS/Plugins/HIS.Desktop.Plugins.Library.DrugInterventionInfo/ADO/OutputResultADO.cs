using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    public class OutputResultADO
    {
        public string Message { get; set; }
        public AlertSeverityLevel AlertLevel { get; set; }
    }

    public enum AlertSeverityLevel
    {
        Normal = 0,
        Warning = 1,
        Contraindicated = 2
    }
}
