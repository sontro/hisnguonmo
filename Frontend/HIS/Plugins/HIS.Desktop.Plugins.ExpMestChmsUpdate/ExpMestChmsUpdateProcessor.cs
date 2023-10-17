using HIS.Desktop.Plugins.ExpMestChmsUpdate.ExpMestChmsUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestChmsUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestChmsUpdate",
        "Tạo xuất chuyển kho thuôc vật tư",
        "Common",
        25,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ExpMestChmsUpdateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestChmsUpdateProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestChmsUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestChmsUpdate behavior = ExpMestChmsUpdateFactory.MakeIExpMestChmsCreate(param, args);
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
