using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisService
{
    class CallModule
    {
        internal const string HisServicePatyList = "HIS.Desktop.Plugins.HisServicePatyList";
        internal const string HisServiceRetyCat = "HIS.Desktop.Plugins.HisServiceRetyCat";
        internal const string HisImportService = "HIS.Desktop.Plugins.HisImportService";
        internal const string RoomService = "HIS.Desktop.Plugins.RoomService";
        internal const string HisServiceHein = "HIS.Desktop.Plugins.HisServiceHein";

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
