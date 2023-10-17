using HIS.Desktop.Plugins.TransactionBillDetail.TransactionBillDetail;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillDetail
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionBillDetail",
        "Phẩn bổ chi phí gián tiếp",
        "Common",
        52,
        "expenseList.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionBillDetailProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionBillDetailProcessor()
        {
            param = new CommonParam();
        }
        public TransactionBillDetailProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionBillDetail behavior = TransactionBillDetailFactory.MakeITransactionBillDetail(param, args);
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
