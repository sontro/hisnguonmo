using HIS.Desktop.Plugins.HisServSegr.HisServSegr;
using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServSegr
{
     [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisServSegr",
        "Nhóm dịch vụ",
        "Common",
        62,
        "nhom-dich-vu.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__COMBO,
        true,
        true)]
    public class HisServSegrProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisServSegrProcessor()
        {
            param = new CommonParam();
        }
        public HisServSegrProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IHisServSegr behavior = HisServSegrFactoty.MakeIRemuneration(param, args);
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
