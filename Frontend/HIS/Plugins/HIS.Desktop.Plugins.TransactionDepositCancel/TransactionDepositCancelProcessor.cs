using HIS.Desktop.Plugins.TransactionDepositCancel.TransactionDepositCancel;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDepositCancel
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionDepositCancel",
        "Hủy thanh toán",
        "Common",
        59,
        "cancelTransaction.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionDepositCancelProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionDepositCancelProcessor()
        {
            param = new CommonParam();
        }
        public TransactionDepositCancelProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionDepositCancel behavior = TransactionDepositCancelBehaviorFactory.MakeITransactionDepositCancel(param, args);
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
