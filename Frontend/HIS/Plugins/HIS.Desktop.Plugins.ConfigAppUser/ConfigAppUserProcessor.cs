using HIS.Desktop.Plugins.ConfigAppUser.ConfigAppUser;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.ConfigAppUser
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ConfigAppUser",
        "Thiết lập cấu hình phần mềm",
        "Common",
        62,
        "cau-hinh.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__COMBO,
        true,
        true)]
    public class ConfigAppUserProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ConfigAppUserProcessor()
        {
            param = new CommonParam();
        }
        public ConfigAppUserProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IConfigAppUser behavior = ConfigAppUserFactory.MakeIConfigAppUser(param, args);
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