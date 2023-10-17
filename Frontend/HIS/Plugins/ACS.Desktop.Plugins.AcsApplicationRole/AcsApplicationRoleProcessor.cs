using ACS.Desktop.Plugins.AcsApplicationRole.AcsApplicationRole;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace ACS.Desktop.Plugins.AcsApplicationRole
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "ACS.Desktop.Plugins.AcsApplicationRole",
        "Thiết lập kho xuất phòng",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class AcsApplicationRoleProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AcsApplicationRoleProcessor()
        {
            param = new CommonParam();
        }
        public AcsApplicationRoleProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IAcsApplicationRole behavior = AcsApplicationRoleFactory.MakeIAcsApplicationRole(param, args);
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