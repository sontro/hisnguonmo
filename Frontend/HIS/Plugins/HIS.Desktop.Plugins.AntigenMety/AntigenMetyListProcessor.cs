
using HIS.Desktop.Plugins.AntigenMety.AntigenMetyList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AntigenMety
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.AntigenMety",
        "Chức năng Kháng nguyên - Vắc xin",
        "Common",
        68,
        "thiet-lap.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class AntigenMetyListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AntigenMetyListProcessor()
        {
            param = new CommonParam();
        }
        public AntigenMetyListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IAntigenMetyList behavior = AntigenMetyListFactory.MakeIMediStockMetyList(param, args);
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
