using HIS.Desktop.Plugins.TransactionList.TransactionList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionList",
        "Danh sách giao dịch",
        "Common",
        68,
        "tai-chinh.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionListProcessor()
        {
            param = new CommonParam();
        }
        public TransactionListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionList behavior = TransactionListFactory.MakeITransactionList(param, args);
                result = behavior != null ? (behavior.Run()) : null;
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
