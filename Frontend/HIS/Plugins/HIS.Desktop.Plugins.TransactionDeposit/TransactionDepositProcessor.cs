using HIS.Desktop.Plugins.TransactionDeposit.TransactionDeposit;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDeposit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionDeposit",
        "Thanh toán",
        "Common",
        59,
        "transactionDeposit.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionDepositProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionDepositProcessor()
        {
            param = new CommonParam();
        }
        public TransactionDepositProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionDeposit behavior = TransactionDepositFactory.MakeITransactionDeposit(param, args);
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
