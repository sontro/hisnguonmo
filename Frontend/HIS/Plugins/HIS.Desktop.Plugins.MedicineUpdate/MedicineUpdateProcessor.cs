using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.MedicineUpdate.MedicineUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.MedicineUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.MedicineUpdate",
    "Sửa thông tin thuốc",
    "Common",
    62,
    "MedicineList.png",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true)]
    public class MedicineUpdateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MedicineUpdateProcessor()
        {
            param = new CommonParam();
        }
        public MedicineUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMedicineUpdate behavior = MedicineUpdateFactory.MakeIMedicineUpdate(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public override bool IsEnable()
        {
            return false;
        }
    }
}
