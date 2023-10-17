using HIS.Desktop.Plugins.InvoiceCreate.InvoiceCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.InvoiceCreate",
        "Tạo hóa đơn",
        "Common",
        59,
        "InvoiceCreate.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class InvoiceCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InvoiceCreateProcessor()
        {
            param = new CommonParam();
        }
        public InvoiceCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IInvoiceCreate behavior = InvoiceCreateFactory.MakeIInvoiceCreate(param, args);
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
