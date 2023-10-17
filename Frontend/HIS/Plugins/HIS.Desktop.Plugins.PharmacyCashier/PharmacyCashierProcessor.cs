using HIS.Desktop.Plugins.PharmacyCashier.PharmacyCashier;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PharmacyCashier
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.PharmacyCashier",
        "Giao dịch",
        "Common",
        57,
        "tai-chinh.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class PharmacyCashierProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public PharmacyCashierProcessor()
        {
            param = new CommonParam();
        }
        public PharmacyCashierProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IPharmacyCashier behavior = PharmacyCashierFactory.MakeIPharmacyCashier(param, args);
                result = (behavior != null) ? behavior.Run() : null;
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
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
