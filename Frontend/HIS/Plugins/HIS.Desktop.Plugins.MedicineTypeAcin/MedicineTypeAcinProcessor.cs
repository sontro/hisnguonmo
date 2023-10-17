using HIS.Desktop.Plugins.MedicineTypeAcin.MedicineTypeAcin;
using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeAcin
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.MedicineTypeAcin",
        "Loại thuốc - hoạt chất",
        "Common",
        62,
        "bidList.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class MedicineTypeAcinProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MedicineTypeAcinProcessor()
        {
            param = new CommonParam();
        }
        public MedicineTypeAcinProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMedicineTypeAcin behavior = MedicineTypeAcinFactory.MakeIImpMestTypeUser(param, args);
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
