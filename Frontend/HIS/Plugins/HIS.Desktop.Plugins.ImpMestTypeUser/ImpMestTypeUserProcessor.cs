
using HIS.Desktop.Plugins.ImpMestTypeUser.ImpMestTypeUser;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestTypeUser
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ImpMestTypeUser",
        "Thiết lập loại nhập - tài khoản",
        "Common",
        68,
        "reading_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ImpMestTypeUserProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImpMestTypeUserProcessor()
        {
            param = new CommonParam();
        }
        public ImpMestTypeUserProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IImpMestTypeUser behavior = ImpMestTypeUserFactory.MakeIImpMestTypeUser(param, args);
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
