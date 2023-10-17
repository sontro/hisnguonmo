using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish
{
    class CallModule
    {
        internal const string TextLibrary = "HIS.Desktop.Plugins.TextLibrary";
        internal const string HisPatientProgram = "HIS.Desktop.Plugins.HisPatientProgram";
        internal const string Bordereau = "HIS.Desktop.Plugins.Bordereau";
        internal const string ListSurgMisuByTreatment = "HIS.Desktop.Plugins.ListSurgMisuByTreatment";
        internal const string OtherForms = "HIS.Desktop.Plugins.OtherForms";
        internal const string AppointmentService = "HIS.Desktop.Plugins.AppointmentService";
        internal const string BedHistory = "HIS.Desktop.Plugins.BedHistory";
        internal const string WorkPlace = "HIS.Desktop.Plugins.HisWorkPlace";
        internal const string EmrDocument = "HIS.Desktop.Plugins.EmrDocument";
        internal const string HisTranPatiTemp = "HIS.Desktop.Plugins.HisTranPatiTemp";
        internal const string InformationAllowGoHome = "HIS.Desktop.Plugins.InformationAllowGoHome";

        internal static void Run(string _moduleLink, long _roomId, long _roomTypeId, List<object> _listObj)
        {
            HIS.Desktop.ModuleExt.PluginInstanceBehavior.ShowModule(_moduleLink, _roomId, _roomTypeId, _listObj);
        }
    }
}
