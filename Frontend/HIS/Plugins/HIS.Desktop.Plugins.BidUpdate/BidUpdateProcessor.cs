using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.BidUpdate.BidUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.BidUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.BidUpdate",
        "Chi tiết gói thầu",
        "Common",
        62,
        "bidList.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class BidUpdateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public BidUpdateProcessor()
        {
            param = new CommonParam();
        }
        public BidUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IBidUpdate behavior = BidUpdateFactory.MakeIControl(param, args);
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
