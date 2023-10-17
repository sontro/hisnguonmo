using HIS.Desktop.Plugins.SetupMedicineTypeWithAcin.SetupMedicineTypeWithAcin;
using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SetupMedicineTypeWithAcin
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.SetupMedicineTypeWithAcin",
        "Thiết lập loại thuốc/hoạt chất",
        "Common",
        62,
        "bidList.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class SetupMedicineTypeWithAcinProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SetupMedicineTypeWithAcinProcessor()
        {
            param = new CommonParam();
        }
        public SetupMedicineTypeWithAcinProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMedicineTypeWA behavior = MedicineTypeWAFactory.MakeIMedicineTypeWA(param, args);
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
