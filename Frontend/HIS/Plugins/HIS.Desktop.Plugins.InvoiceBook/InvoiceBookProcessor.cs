using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Plugins.InvoiceBook.InvoiceBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Core;
using Inventec.Desktop.Common.Modules;

namespace Inventec.Desktop.Plugins.InvoiceBook
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.InvoiceBook",
       "Sổ hóa đơn",
       "Common",
       34,
       "so-hoa-don.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true
       )
    ]
    public class InvoiceBookProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InvoiceBookProcessor()        
        {
            param = new CommonParam();
        }
        public InvoiceBookProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IInvoiceBook behavior = InvoiceBookFactory.MakeIInvoiceBook(param, args);
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
