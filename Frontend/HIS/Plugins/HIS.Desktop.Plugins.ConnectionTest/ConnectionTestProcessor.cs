using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ConnectionTest;
using HIS.Desktop.Plugins.ConnectionTest.ConnectionTest;

namespace HIS.Desktop.Plugins.ConnectionTest
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ConnectionTest",
        "Kết nối xét nghiệm",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ConnectionTestProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ConnectionTestProcessor()
        {
            param = new CommonParam();
        }
        public ConnectionTestProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IConnectionTest behavior = ConnectionTestFactory.MakeIConnectionTest(param, args);
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
