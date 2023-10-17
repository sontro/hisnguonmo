using HIS.Desktop.Plugins.ServiceMaty.ServiceMaty;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.ServiceMaty
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ServiceMaty",
        "Thiết lập hao phí vật tư dịch vụ",
        "Common",
        62,
        "thiet-lap.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ServiceMatyProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ServiceMatyProcessor()
        {
            param = new CommonParam();
        }
        public ServiceMatyProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IServiceMaty behavior = ServiceMatyFactory.MakeIServiceMaty(param, args);
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