using HIS.Desktop.Plugins.AllocateExpeExpMestCreate.AllocateExpeExpMestCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateExpeExpMestCreate.AllocateExpeExpMestCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.AllocateExpeExpMestCreate",
        "Tạo xuất hao phí",
        "Common",
        27,
        "ExpeExpMest.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    class AllocateExpeExpMestCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AllocateExpeExpMestCreateProcessor()
        {
            param = new CommonParam();
        }
        public AllocateExpeExpMestCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IAllocateExpeExpMestCreate behavior = AllocateExpeExpMestCreateFactory.MakeIExpMestSaleCreate(param, args);
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
