using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExportMestMedicine
{
    class CallModule
    {
        internal const string MobaDepaCreate = "HIS.Desktop.Plugins.MobaDepaCreate";
        internal const string ImpMestChmsCreate = "HIS.Desktop.Plugins.ImpMestChmsCreate";
        internal const string EventLog = "Inventec.Desktop.Plugins.EventLog";
        internal const string ExpMestViewDetail = "HIS.Desktop.Plugins.ExpMestViewDetail";
        internal const string AggrExpMestDetail = "HIS.Desktop.Plugins.AggrExpMestDetail";
        internal const string ApproveAggrExpMest = "HIS.Desktop.Plugins.ApproveAggrExpMest";
        internal const string ExpMestOtherExport = "HIS.Desktop.Plugins.ExpMestOtherExport";
        internal const string ExpMestSaleCreate = "HIS.Desktop.Plugins.ExpMestSaleCreate";
        internal const string ExpMestSaleCreateV2 = "HIS.Desktop.Plugins.ExpMestSaleCreateV2";
        internal const string ExpMestChmsUpdate = "HIS.Desktop.Plugins.ExpMestChmsUpdate";
        internal const string ExpMestDepaUpdate = "HIS.Desktop.Plugins.ExpMestDepaUpdate";
        internal const string ManuExpMestCreate = "HIS.Desktop.Plugins.ManuExpMestCreate";
        internal const string BrowseExportTicket = "HIS.Desktop.Plugins.BrowseExportTicket";
        internal const string MobaBloodCreate = "HIS.Desktop.Plugins.MobaBloodCreate";
        internal const string MobaSaleCreate = "HIS.Desktop.Plugins.MobaSaleCreate";
        internal const string AssignServiceTest = "HIS.Desktop.Plugins.AssignServiceTest";
        internal const string AssignServiceTestMulti = "HIS.Desktop.Plugins.AssignServiceTestMulti";
        internal const string MobaExamPresCreate = "HIS.Desktop.Plugins.MobaExamPresCreate";
        internal const string ApprovalExpMestBcs = "HIS.Desktop.Plugins.ApproveExpMestBCS";
        internal const string DrugStoreDebt = "HIS.Desktop.Plugins.DrugStoreDebt";

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
