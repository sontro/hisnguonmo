using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CompareBhytInfo
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.CompareBhytInfo",
        "Đối chiếu danh mục BHYT",
        "Common",
        62,
        "",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class CompareBhytInfoProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public CompareBhytInfoProcessor()
        {
            param = new CommonParam();
        }
        public CompareBhytInfoProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                CompareBhytInfo.ICompareBhytInfo behavior = CompareBhytInfo.CompareBhytInfoFactory.MakeIControl(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
