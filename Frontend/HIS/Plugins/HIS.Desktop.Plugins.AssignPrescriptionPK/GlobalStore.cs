
using System.Collections.Generic;
namespace HIS.Desktop.Plugins.AssignPrescriptionPK
{
    public class GlobalStore
    {
        internal static string ClientSessionKey { get; set; }
        internal static bool IsTreatmentIn { get; set; }
        internal static bool IsCabinet { get; set; }
        internal static bool IsExecutePTTT { get; set; }
        internal static List<MOS.EFMODEL.DataModels.HIS_MEST_METY_UNIT> HisMestMetyUnit { get; set; }
    }
}
