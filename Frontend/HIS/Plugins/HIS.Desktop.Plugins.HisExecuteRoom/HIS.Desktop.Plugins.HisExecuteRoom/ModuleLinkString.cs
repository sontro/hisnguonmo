using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExecuteRoom
{
    class CallModule
    {
        internal const string HisRoomTime = "HIS.Desktop.Plugins.HisRoomTime";
        internal const string PatientTypeRoom = "HIS.Desktop.Plugins.PatientTypeRoom";
        internal const string ServiceRoom = "HIS.Desktop.Plugins.RoomService";
        internal const string MedicineTypeRoom = "HIS.Desktop.Plugins.MedicineTypeRoom";
        internal const string ExroRoom = "HIS.Desktop.Plugins.ExroRoom";
        internal const string HisImportExecuteRoom = "HIS.Desktop.Plugins.HisImportExecuteRoom";

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
