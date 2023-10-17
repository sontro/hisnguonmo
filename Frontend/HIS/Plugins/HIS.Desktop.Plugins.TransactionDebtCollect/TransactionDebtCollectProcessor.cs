using HIS.Desktop.Plugins.TransactionDebtCollect.TransactionDebtCollect;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDebtCollect
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionDebtCollect",
        "Thu nợ",
        "Common",
        59,
        "thanh-toan.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionDebtCollectProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionDebtCollectProcessor()
        {
            param = new CommonParam();
        }
        public TransactionDebtCollectProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionDebtCollect behavior = TransactionDebtCollectFactory.MakeITransactionBill(param, args);
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
