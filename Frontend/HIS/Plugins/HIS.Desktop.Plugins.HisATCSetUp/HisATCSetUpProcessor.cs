using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Core;
using HIS.Desktop.Plugins.HisATCSetUp.ATCSetUp;

namespace HIS.Desktop.Plugins.HisATCSetUp
{
   [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisATCSetUp",
        "Nhóm dịch vụ",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class HisATCSetUpProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisATCSetUpProcessor()
        {
            param = new CommonParam();
        }
        public HisATCSetUpProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IATCSetUp behavior = ATCSetUpFactory.MakeIRemuneration(param, args);
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
