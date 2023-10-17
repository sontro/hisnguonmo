using HIS.Desktop.Plugins.ExpMestSaleEdit.ExpMestSaleEdit;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestSaleEdit",
        "Sửa xuất chuyển kho thuôc vật tư",
        "Common",
        27,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    class ExpMestSaleEditProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestSaleEditProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestSaleEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestSaleEdit behavior = ExpMestSaleEditFactory.MakeIExpMestSaleEdit(param, args);
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
