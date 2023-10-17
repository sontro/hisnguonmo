using ACS.Desktop.Plugins.AcsControlRole.AcsControlRole;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace ACS.Desktop.Plugins.AcsControlRole
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "ACS.Desktop.Plugins.AcsControlRole",
        "Thiết lập kho xuất phòng",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class AcsControlRoleProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AcsControlRoleProcessor()
        {
            param = new CommonParam();
        }
        public AcsControlRoleProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IAcsControlRole behavior = AcsControlRoleFactory.MakeIAcsControlRole(param, args);
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