using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ListPresBloodConfirm
{
    class CallModule
    {
        internal const string ManuImpMestEdit = "HIS.Desktop.Plugins.ManuImpMestEdit";
        internal const string ManuExpMestCreate = "HIS.Desktop.Plugins.ManuExpMestCreate";
        internal const string EventLog = "Inventec.Desktop.Plugins.EventLog";
        internal const string ApproveAggrImpMest = "HIS.Desktop.Plugins.ApproveAggrImpMest";
        internal const string ManuImpMestUpdate = "HIS.Desktop.Plugins.ManuImpMestUpdate";
        internal const string CallPatientTypeAlter = "HIS.Desktop.Plugins.CallPatientTypeAlter";
        internal const string BloodImpMestUpdate = "HIS.Desktop.Plugins.ImportBlood";
        internal const string ApproveAggrExpMest = "HIS.Desktop.Plugins.ApproveAggrExpMest";
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
