using HIS.Desktop.Plugins.TransactionBillTwoInOne.TransactionBillTwoInOne;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillTwoInOne
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionBillTwoInOne",
        "Thanh toán",
        "Common",
        59,
        "transactionBill.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionBillTwoInOneProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionBillTwoInOneProcessor()
        {
            param = new CommonParam();
        }
        public TransactionBillTwoInOneProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionBillTwoInOne behavior = TransactionBillTwoInOneFactory.MakeITransactionBillTwoInOne(param, args);
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
