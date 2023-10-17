using HIS.Desktop.Plugins.TransactionDebt.TransactionDebt;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDebt
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionDebt",
        "Thanh toán",
        "Common",
        59,
        "thanh-toan.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class frmTransactionDebtProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public frmTransactionDebtProcessor()
        {
            param = new CommonParam();
        }
        public frmTransactionDebtProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionDebt behavior = TransactionDebtFactory.MakeITransactionBill(param, args);
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
