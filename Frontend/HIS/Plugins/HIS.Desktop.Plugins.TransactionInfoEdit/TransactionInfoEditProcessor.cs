using HIS.Desktop.Plugins.TransactionInfoEdit.TransactionInfoEdit;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionInfoEdit
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TransactionInfoEdit",
        "Thanh toán",
        "Common",
        59,
        "TransactionInfoEdit.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TransactionInfoEditProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionInfoEditProcessor()
        {
            param = new CommonParam();
        }
        public TransactionInfoEditProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITransactionInfoEdit behavior = TransactionInfoEditFactory.MakeITransactionInfoEdit(param, args);
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
