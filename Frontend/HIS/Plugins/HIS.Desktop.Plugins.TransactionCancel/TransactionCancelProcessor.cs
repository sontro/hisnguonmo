using HIS.Desktop.Plugins.TransactionCancel.TransactionCancel;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionCancel
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionCancel",
        "Hủy giao dịch",
        "Common",
        59,
        "cancelTransaction.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionCancelProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionCancelProcessor()
        {
            param = new CommonParam();
        }
        public TransactionCancelProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionCancel behavior = TransactionCancelBehaviorFactory.MakeITransactionDepositCancel(param, args);
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
