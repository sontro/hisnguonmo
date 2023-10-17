using HIS.Desktop.Plugins.XMLViewer130.XMLViewer130;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.XMLViewer130
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.XMLViewer130",
        "Hủy giao dịch",
        "Common",
        59,
        "cancelTransaction.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class XMLViewer130Processor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public XMLViewer130Processor()
        {
            param = new CommonParam();
        }
        public XMLViewer130Processor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IXMLViewer130 behavior = XMLViewer130BehaviorFactory.MakeITransactionDepositCancel(args);
                result = (behavior != null) ? behavior.Run() : null;
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
