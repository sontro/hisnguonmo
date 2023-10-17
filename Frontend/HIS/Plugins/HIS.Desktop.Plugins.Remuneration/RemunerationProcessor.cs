using HIS.Desktop.Plugins.Remuneration.Remuneration;
using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Remuneration
{
     [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.Remuneration",
        "Thiết lập phụ cấp dịch vụ",
        "Common",
        62,
        "dich-vu.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class RemunerationProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RemunerationProcessor()
        {
            param = new CommonParam();
        }
        public RemunerationProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRemuneration behavior = RemunerationFactoty.MakeIRemuneration(param, args);
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
