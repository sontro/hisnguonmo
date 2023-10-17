using HIS.Desktop.Plugins.ExpMestChmsCreate.ExpMestChmsCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestChmsCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestChmsCreate",
        "Tạo xuất chuyển kho thuôc vật tư",
        "Common",
        25,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ExpMestChmsCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestChmsCreateProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestChmsCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestChmsCreate behavior = ExpMestChmsCreateFactory.MakeIExpMestChmsCreate(param, args);
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
