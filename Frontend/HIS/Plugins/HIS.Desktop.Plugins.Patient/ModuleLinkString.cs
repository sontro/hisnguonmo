using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Patient
{
    class CallModule
    {
        internal const string TreatmentHistory = "HIS.Desktop.Plugins.TreatmentHistory";
        internal const string PatientUpdate = "HIS.Desktop.Plugins.PatientUpdate";
        internal const string PatientInfo = "HIS.Desktop.Plugins.PatientInfo";
        internal const string ServiceReqList = "HIS.Desktop.Plugins.ServiceReqList";
        internal const string HisPatientProgram = "HIS.Desktop.Plugins.HisPatientProgram";
        internal const string ScnPersonalHealth = "HIS.Desktop.Plugins.ScnPersonalHealth";
        internal const string UpdatePatientExt = "HIS.Desktop.Plugins.UpdatePatientExt";
        internal const string ScnAccidentHurt = "HIS.Desktop.Plugins.ScnAccidentHurt";
        internal const string ScnDisease = "HIS.Desktop.Plugins.ScnDisease";
        internal const string ScnHealthRisk = "HIS.Desktop.Plugins.ScnHealthRisk";
        internal const string ScnVaccination = "HIS.Desktop.Plugins.ScnVaccination";
        internal const string ScnSurgery = "HIS.Desktop.Plugins.ScnSurgery";
        internal const string EventLog = "Inventec.Desktop.Plugins.EventLog";
        internal const string ScnNutrition = "SCN.Desktop.Plugins.ScnNutrition";
        internal const string ScnDeath = "HIS.Desktop.Plugins.ScnDeath";

        public CallModule(string _moduleLink, long _roomId, long _roomTypeId, List<object> _listObj)
        {
            CallModuleProcess(_moduleLink, _roomId, _roomTypeId, _listObj);
        }

        private void CallModuleProcess(string _moduleLink, long _roomId, long _roomTypeId, List<object> _listObj)
        {
            HIS.Desktop.ModuleExt.PluginInstanceBehavior.ShowModule(_moduleLink, _roomId, _roomTypeId, _listObj);
        }
    }
}
