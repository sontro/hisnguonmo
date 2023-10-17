using HIS.Desktop.Plugins.RequestForUsingAccountBook.RequestAccountBook;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RequestForUsingAccountBook
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.RequestForUsingAccountBook",
        "Yêu cầu sử dụng sổ biên lai/hóa đơn",
        "Common",
        59,
        "",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class RequestForUsingAccountBookProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RequestForUsingAccountBookProcessor()
        {
            param = new CommonParam();
        }

        public RequestForUsingAccountBookProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRequestAccountBook behavior = RequestAccountBookFactory.MakeIRequestAccountBook(param, args);
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
