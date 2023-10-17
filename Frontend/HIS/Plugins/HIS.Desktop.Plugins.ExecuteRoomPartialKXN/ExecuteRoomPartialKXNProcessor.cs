using HIS.Desktop.Plugins.ExecuteRoomPartialKXN.ExecuteRoomPartialKXN;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoomPartialKXN
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExecuteRoomPartialKXN",
        "Phòng xử lý xét nghiệm",
        "Common",
        27,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    class ExecuteRoomPartialKXNProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExecuteRoomPartialKXNProcessor()
        {
            param = new CommonParam();
        }
        public ExecuteRoomPartialKXNProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExecuteRoomPartialKXN behavior = ExecuteRoomPartialKXNFactory.MakeIExecuteRoomPartialKXN(param, args);
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
