using HIS.Desktop.Plugins.TransactionBillInfoEdit.TransactionBillInfoEdit;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillInfoEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionBillInfoEdit",
        "Sửa thông tin hóa đơn",
        "Common",
        59,
        "TransactionBillInfoEdit.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionBillInfoEditProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionBillInfoEditProcessor()
        {
            param = new CommonParam();
        }
        public TransactionBillInfoEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionBillInfoEdit behavior = TransactionBillInfoEditFactory.MakeITransactionBillInfoEdit(param, args);
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
