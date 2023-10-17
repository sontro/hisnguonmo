using HIS.Desktop.Plugins.RevenueDeleteByPeriod.RevenueDeleteByPeriod;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RevenueDeleteByPeriod
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.RevenueDeleteByPeriod",
        "Thanh toán",
        "Common",
        59,
        "RevenueDeleteByPeriod.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class RevenueDeleteByPeriodProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RevenueDeleteByPeriodProcessor()
        {
            param = new CommonParam();
        }
        public RevenueDeleteByPeriodProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRevenueDeleteByPeriod behavior = RevenueDeleteByPeriodFactory.MakeIRevenueDeleteByPeriod(param, args);
                result = (behavior != null) ? behavior.Run() : null;
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
