using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAccountBookList
{
    class CallModule
    {
        internal const string HisUserAccountBook = "HIS.Desktop.Plugins.HisUserAccountBook";
        internal const string HisCaroAccountBook = "HIS.Desktop.Plugins.HisCaroAccountBook";
        internal const string HisAccountBookListImport = "HIS.Desktop.Plugins.HisAccountBookListImport";

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
