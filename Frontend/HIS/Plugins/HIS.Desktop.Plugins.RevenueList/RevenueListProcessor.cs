using HIS.Desktop.Plugins.RevenueList.RevenueList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RevenueList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.RevenueList",
        "Danh Sách Thu",
        "Common",
        53,
        "revenuelist.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class RevenueListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RevenueListProcessor()
        {
            param = new CommonParam();
        }
        public RevenueListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRevenueList behavior = RevenueListFactory.MakeIRevenueList(param, null);
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
            return true;
        }
    }
}
