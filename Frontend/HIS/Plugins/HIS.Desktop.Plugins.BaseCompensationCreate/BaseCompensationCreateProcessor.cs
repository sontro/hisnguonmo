using HIS.Desktop.Plugins.BaseCompensationCreate.BaseCompensationCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.BaseCompensationCreate",
        "Tạo xuất bù cơ số",
        "Common",
        25,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class BaseCompensationCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public BaseCompensationCreateProcessor()
        {
            param = new CommonParam();
        }
        public BaseCompensationCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IBaseCompensationCreate behavior = BaseCompensationCreateFactory.MakeIBaseCompensationCreate(param, args);
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
