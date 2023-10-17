using HIS.Desktop.Plugins.HisMedicineTypeAcin.HisMedicineTypeAcin;
using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicineTypeAcin
{
     [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisMedicineTypeAcin",
        "Nhóm dịch vụ",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class HisMedicineTypeAcinProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisMedicineTypeAcinProcessor()
        {
            param = new CommonParam();
        }
        public HisMedicineTypeAcinProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IHisMedicineTypeAcin behavior = HisMedicineTypeAcinFactoty.MakeIRemuneration(param, args);
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
