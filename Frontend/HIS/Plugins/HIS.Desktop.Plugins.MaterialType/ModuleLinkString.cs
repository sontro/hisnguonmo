using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialType
{
    class CallModule
    {
        internal const string MaterialTypeCreate = "HIS.Desktop.Plugins.MaterialTypeCreate";
        internal const string HisImportMaterialType = "HIS.Desktop.Plugins.HisImportMaterialType";
        internal const string HisServiceHein = "HIS.Desktop.Plugins.HisServiceHein";
        internal const string PriceListExport = "HIS.Desktop.Plugins.ExportMediMatePriceList";

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
