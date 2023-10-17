using HIS.Desktop.Plugins.SessionInfo.SessionInfo;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SessionInfo
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.SessionInfo",
        "Phẩn bổ chi phí gián tiếp",
        "Common",
        52,
        "expenseList.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class SessionInfoProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SessionInfoProcessor()
        {
            param = new CommonParam();
        }
        public SessionInfoProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ISessionInfo behavior = SessionInfoFactory.MakeITransactionBillDetail(param, args);
                result = behavior != null ? (behavior.Run()) : null;
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
