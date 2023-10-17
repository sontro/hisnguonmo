using HIS.Desktop.Plugins.CompensationByBaseCreate.CompensationByBaseCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CompensationByBaseCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.CompensationByBaseCreate",
        "Tạo xuất bù cơ số",
        "Common",
        25,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class CompensationByBaseCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public CompensationByBaseCreateProcessor()
        {
            param = new CommonParam();
        }
        public CompensationByBaseCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ICompensationByBaseCreate behavior = CompensationByBaseCreateFactory.MakeICompensationByBaseCreate(param, args);
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
