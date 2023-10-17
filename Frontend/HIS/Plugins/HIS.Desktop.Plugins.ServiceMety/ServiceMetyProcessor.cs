using HIS.Desktop.Plugins.ServiceMety.ServiceMety;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.ServiceMety
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ServiceMety",
        "Thiết lập hao phí thuốc dịch vụ",
        "Common",
        62,
        "thiet-lap.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ServiceMetyProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ServiceMetyProcessor()
        {
            param = new CommonParam();
        }
        public ServiceMetyProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IServiceMety behavior = ServiceMetyFactory.MakeIServiceMety(param, args);
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