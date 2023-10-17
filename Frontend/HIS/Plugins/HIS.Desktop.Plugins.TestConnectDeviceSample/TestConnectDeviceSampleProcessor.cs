using HIS.Desktop.Plugins.TestConnectDeviceSample.TestConnectDeviceSample;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestConnectDeviceSample
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TestConnectDeviceSample",
        "Kết nối thiết bị lấy mẫu bệnh phẩm",
        "Commmon",
        12,
        "sale.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class TestConnectDeviceSampleProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;

        public TestConnectDeviceSampleProcessor()
        {
            param = new CommonParam();
        }

        public TestConnectDeviceSampleProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null) ? paramBusiness : new CommonParam();
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITestConnectDeviceSample behavior = TestConnectDeviceSampleFactory.MakeITestConnectDeviceSample(param, args);
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
            return true;
        }
    }
}
