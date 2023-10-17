using HIS.Desktop.Plugins.ExpMestBCSCreate.ExpMestBCSCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestBCSCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestBCSCreate",
        "Tạo xuất bù cơ số",
        "Common",
        25,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ExpMestBCSCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestBCSCreateProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestBCSCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestBCSCreate behavior = ExpMestBCSCreateFactory.MakeIExpMestBCSCreate(param, args);
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
