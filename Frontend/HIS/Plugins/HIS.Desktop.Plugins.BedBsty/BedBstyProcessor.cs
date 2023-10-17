using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.BedBsty;
using HIS.Desktop.Plugins.BedBsty.BedBsty;

namespace HIS.Desktop.Plugins.BedBsty
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.BedBsty",
        "Thiết lập Vai trò thực hiện-Người dùng",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class BedBstyProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public BedBstyProcessor()
        {
            param = new CommonParam();
        }
        public BedBstyProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IBedBsty behavior = BedBstyFactory.MakeIBedBsty(param, args);
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
