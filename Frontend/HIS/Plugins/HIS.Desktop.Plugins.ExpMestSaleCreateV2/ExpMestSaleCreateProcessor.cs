using HIS.Desktop.Plugins.ExpMestSaleCreateV2.ExpMestSaleCreateV2;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreateV2
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestSaleCreateV2",
        "Tạo xuất chuyển kho thuôc vật tư",
        "Common",
        27,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    class ExpMestSaleCreateV2Processor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestSaleCreateV2Processor()
        {
            param = new CommonParam();
        }
        public ExpMestSaleCreateV2Processor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestSaleCreateV2 behavior = ExpMestSaleCreateV2Factory.MakeIExpMestSaleCreateV2(param, args);
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
