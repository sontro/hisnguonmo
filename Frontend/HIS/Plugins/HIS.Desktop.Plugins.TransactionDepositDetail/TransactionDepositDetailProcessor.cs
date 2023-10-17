using HIS.Desktop.Plugins.TransactionDepositDetail.TransactionDepositDetail;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDepositDetail
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionDepositDetail",
        "Chi tiết tạm ứng",
        "Common",
        52,
        "expenseList.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionDepositDetailProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionDepositDetailProcessor()
        {
            param = new CommonParam();
        }
        public TransactionDepositDetailProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionDepositDetail behavior = TransactionDepositDetailFactory.MakeITransactionDepositDetail(param, args);
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
