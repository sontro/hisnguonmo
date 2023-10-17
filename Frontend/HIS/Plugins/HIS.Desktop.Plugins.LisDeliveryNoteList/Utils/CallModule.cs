using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIS.EFMODEL.DataModels;
namespace HIS.Desktop.Plugins.LisDeliveryNoteList.Utils
{
    public class CallModule
    {    
        internal const string ExpMestViewDetail = "HIS.Desktop.Plugins.ExpMestViewDetail";
        internal const string AggrExpMestDetail = "HIS.Desktop.Plugins.AggrExpMestDetail";

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
