using HIS.Desktop.Plugins.ImpMestCreate.ImpMestCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ImpMestCreate",
        "Nhập thuốc vật tư",
        "Common",
        57,
        "nhap-kho.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ImpMestCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImpMestCreateProcessor()
        {
            param = new CommonParam();
        }
        public ImpMestCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IImpMestCreate behavior = ImpMestCreateFactory.MakeIImpMestCreate(param, args);
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
