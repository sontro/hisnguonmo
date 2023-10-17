using HIS.Desktop.Plugins.Optometrist.Optometrist;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Optometrist
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.Optometrist",
           "Đo thị lực",
           "Common",
           31,
           "newitem_32x32.png",
           "A",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
    ]
    public class OptometristProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public OptometristProcessor()
        {
            param = new CommonParam();
        }

        public OptometristProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IOptometrist behavior = OptometristFactory.MakeIOptometrist(param, args);
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
