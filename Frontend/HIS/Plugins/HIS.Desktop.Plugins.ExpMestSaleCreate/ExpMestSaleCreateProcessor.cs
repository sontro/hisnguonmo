using HIS.Desktop.Plugins.ExpMestSaleCreate.ExpMestSaleCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestSaleCreate",
        "Tạo xuất chuyển kho thuôc vật tư",
        "Common",
        27,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    class ExpMestSaleCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestSaleCreateProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestSaleCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestSaleCreate behavior = ExpMestSaleCreateFactory.MakeIExpMestSaleCreate(param, args);
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
