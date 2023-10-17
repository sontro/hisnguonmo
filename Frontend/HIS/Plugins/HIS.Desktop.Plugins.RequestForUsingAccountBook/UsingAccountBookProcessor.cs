using HIS.Desktop.Plugins.RequestForUsingAccountBook.UsingAccountBook;
using Inventec.Core;
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
        "",
        "Common",
        62,
        "technology_32x32.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class UsingAccountBookProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public UsingAccountBookProcessor()
        {
            param = new CommonParam();
        }
        public UsingAccountBookProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IUsingAccountBook behavior = UsingAccountBookFactory.MakeIUsingAccountBook(param, args);
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
