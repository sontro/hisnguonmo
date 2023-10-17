using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeCreate
{
    class CallModule
    {
        
        internal const string HisMedicineLine = "HIS.Desktop.Plugins.HisMedicineLine";
        internal const string HisManufacturer = "HIS.Desktop.Plugins.HisManufacturer";
        internal const string HisDosageForm = "HIS.Desktop.Plugins.HisDosageForm";
        internal const string HisHowToUse = "HIS.Desktop.Plugins.HisHtu";
        internal const string HisMedicineUseForm = "HIS.Desktop.Plugins.HisMedicineUseForm";
        internal const string HisPackingType = "HIS.Desktop.Plugins.HisPackingType";
        internal const string HisMedicineTypeAcin = "HIS.Desktop.Plugins.HisMedicineTypeAcin";
        internal const string HisServicePatyList = "HIS.Desktop.Plugins.HisServicePatyList";
        internal const string HisServiceHein = "HIS.Desktop.Plugins.HisServiceHein";
        internal const string MedicineTypeCreateParent = "HIS.Desktop.Plugins.MedicineTypeCreateParent";
        internal const string HisATC = "HIS.Desktop.Plugins.HisATCSetUp";
        internal const string HisSourceMedicine = "HIS.Desktop.Plugins.HisSourceMedicine";

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
