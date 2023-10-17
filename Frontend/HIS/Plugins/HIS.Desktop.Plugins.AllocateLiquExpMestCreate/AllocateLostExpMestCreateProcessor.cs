using HIS.Desktop.Plugins.AllocateLiquExpMestCreate.AllocateLiquExpMestCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateLiquExpMestCreate.AllocateLiquExpMestCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.AllocateLiquExpMestCreate",
        "Tạo xuất mất mát",
        "Common",
        27,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    class AllocateLostExpMestCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AllocateLostExpMestCreateProcessor()
        {
            param = new CommonParam();
        }
        public AllocateLostExpMestCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IAllocateLiquExpMestCreate behavior = AllocateLiquExpMestCreateFactory.MakeIExpMestSaleCreate(param, args);
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
