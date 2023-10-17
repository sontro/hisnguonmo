using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialTypeCreate
{
    class CallModule
    {
        internal const string HisManufacturer = "HIS.Desktop.Plugins.HisManufacturer";
        internal const string HisPackingType = "HIS.Desktop.Plugins.HisPackingType";
        internal const string HisServicePatyList = "HIS.Desktop.Plugins.HisServicePatyList";
        internal const string MaterialTypeCreateParent = "HIS.Desktop.Plugins.MaterialTypeCreateParent";
        internal const string MedicineTypeCreateParent = "HIS.Desktop.Plugins.MedicineTypeCreateParent";

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
