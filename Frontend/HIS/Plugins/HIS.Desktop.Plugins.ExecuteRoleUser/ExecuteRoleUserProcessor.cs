using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ExecuteRoleUser;
using HIS.Desktop.Plugins.ExecuteRoleUser.ExecuteRoleUser;

namespace HIS.Desktop.Plugins.ExecuteRoleUser
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExecuteRoleUser",
        "Thiết lập Vai trò thực hiện-Người dùng",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ExecuteRoleUserProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExecuteRoleUserProcessor()
        {
            param = new CommonParam();
        }
        public ExecuteRoleUserProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExecuteRoleUser behavior = ExecuteRoleUserFactory.MakeIExecuteRoleUser(param, args);
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
