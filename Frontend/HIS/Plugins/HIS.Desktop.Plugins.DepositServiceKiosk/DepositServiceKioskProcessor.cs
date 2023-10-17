using HIS.Desktop.Plugins.DepositServiceKiosk.DepositServiceKiosk;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DepositServiceKiosk
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.DepositServiceKiosk",
        "Thu nợ",
        "Common",
        59,
        "thanh-toan.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class DepositServiceKioskProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public DepositServiceKioskProcessor()
        {
            param = new CommonParam();
        }
        public DepositServiceKioskProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IDepositServiceKiosk behavior = DepositServiceKioskFactory.MakeITransactionBill(param, args);
                result = (behavior != null) ? behavior.Run() : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public override bool IsEnable()
        {
            return false;
        }
    }
}
