using HIS.Desktop.Plugins.ExpMestDepaUpdate.ExpMestDepaUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestDepaUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestDepaUpdate",
        "Sửa phiếu xuất hao phí khoa phòng",
        "Common",
        25,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ExpMestDepaUpdateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestDepaUpdateProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestDepaUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestDepaUpdate behavior = ExpMestDepaUpdateFactory.MakeIExpMestChmsCreate(param, args);
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
