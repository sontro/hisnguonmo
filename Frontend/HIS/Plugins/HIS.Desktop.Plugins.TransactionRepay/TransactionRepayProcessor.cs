using HIS.Desktop.Plugins.TransactionRepay.TransactionRepay;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionRepay
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionRepay",
        "Thanh toán",
        "Common",
        59,
        "TransactionRepay.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionRepayProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionRepayProcessor()
        {
            param = new CommonParam();
        }
        public TransactionRepayProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionRepay behavior = TransactionRepayFactory.MakeITransactionRepay(param, args);
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
