using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCareTemp
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisCareTemp",
        "Mẫu chăm sóc",
        "Common",
        62,
        "",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    class HisCareTempProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisCareTempProcessor()
        {
            param = new CommonParam();
        }
        public HisCareTempProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                CareTemp.ICareTemp behavior = CareTemp.CareTempFactory.MakeIControl(param, args);
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
