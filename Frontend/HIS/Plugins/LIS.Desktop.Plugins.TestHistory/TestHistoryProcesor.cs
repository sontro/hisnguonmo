using HIS.Desktop.Plugins.TestHistory.TestHistory;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.TestHistory
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "LIS.Desktop.Plugins.TestHistory",
        "Thanh toán",
        "Common",
        59,
        "thanh-toan.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TestHistoryProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TestHistoryProcessor()
        {
            param = new CommonParam();
        }
        public TestHistoryProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITestHistory behavior = TestHistoryFactory.MakeITestHistory(param, args);
                result = (behavior != null) ? behavior.Run() : null;
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
