using HIS.Desktop.Plugins.InvoiceCreateForTreatment.InvoiceCreateForTreatment;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceCreateForTreatment
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.InvoiceCreateForTreatment",
        "Tạo hóa đơn cho hồ sơ viện phí",
        "Common",
        59,
        "InvoiceCreateForTreatment.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class InvoiceCreateForTreatmentProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InvoiceCreateForTreatmentProcessor()
        {
            param = new CommonParam();
        }
        public InvoiceCreateForTreatmentProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IInvoiceCreateForTreatment behavior = InvoiceCreateForTreatmentFactory.MakeIInvoiceCreateForTreatment(param, args);
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
