using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillListPrint
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
             "HIS.Desktop.Plugins.TransactionBillListPrint",
           "In sổ hóa đơn",
           "Common",
           16,
           "giao-dich.png",
           "E",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
       ]
    public class TransactionBillListPrintProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TransactionBillListPrintProcessor()
        {
            param = new CommonParam();
        }
        public TransactionBillListPrintProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                TransactionBillListPrint.ITransactionBillListPrint behavior = TransactionBillListPrint.TransactionBillListPrintFactory.MakeITransactionBillListPrint(param, args);
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
            return true;
        }
    }
}
