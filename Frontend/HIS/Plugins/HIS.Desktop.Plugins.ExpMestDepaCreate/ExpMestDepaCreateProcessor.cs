using HIS.Desktop.Plugins.ExpMestDepaCreate.ExpMestDepaCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestDepaCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestDepaCreate",
        "Tạo xuất chuyển kho thuôc vật tư",
        "Common",
        27,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ExpMestDepaCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestDepaCreateProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestDepaCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestDepaCreate behavior = ExpMestDepaCreateFactory.MakeIExpMestDepaCreate(param, args);
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
