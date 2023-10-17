using HIS.Desktop.Plugins.SampleInfo.SampleInfo;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.SampleInfo
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "LIS.Desktop.Plugins.SampleInfo",
        "Thanh toán",
        "Common",
        59,
        "thanh-toan.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class SampleInfoProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SampleInfoProcessor()
        {
            param = new CommonParam();
        }
        public SampleInfoProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ISampleInfo behavior = SampleInfoFactory.MakeISampleInfo(param, args);
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
