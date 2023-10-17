using HIS.Desktop.Plugins.RequestDeposit.RequestDeposit;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RequestDeposit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.RequestDeposit",
        "Yêu cầu tạm ứng",
        "Common",
        25,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class RequestDepositProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RequestDepositProcessor()
        {
            param = new CommonParam();
        }
        public RequestDepositProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRequestDeposit behavior = RequestDepositFactory.MakeIRequestDeposit(param, args);
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
