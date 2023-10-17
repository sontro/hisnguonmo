using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT
{
    public class GlobalStore
    {
        internal static string ClientSessionKey { get; set; }
        internal static bool IsTreatmentIn { get; set; }
        internal static bool IsCabinet { get; set; }
        internal static bool IsExecutePTTT { get; set; }
    }
}
