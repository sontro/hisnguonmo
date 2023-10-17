using HIS.Desktop.Plugins.AllocationIndrectFee.AllocationIndrectFee;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocationIndrectFee
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.AllocationIndrectFee",
        "Phẩn bổ chi phí gián tiếp",
        "Common",
        52,
        "expenseList.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class AllocationIndrectFeeProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AllocationIndrectFeeProcessor()
        {
            param = new CommonParam();
        }
        public AllocationIndrectFeeProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IAllocationIndrectFee behavior = AllocationIndrectFeeFactory.MakeIAllocationIndrectFee(param, args);
                result = behavior != null ? (behavior.Run()) : null;
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
