using HIS.Desktop.Plugins.ConfigPrinter.ConfigPrinter;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfigPrinter
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ConfigPrinter",
        "Thanh toán",
        "Common",
        23,
        "thiet-lap.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ConfigPrinterProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ConfigPrinterProcessor()
        {
            param = new CommonParam();
        }
        public ConfigPrinterProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IConfigPrinter behavior = ConfigPrinterFactory.MakeIConfigPrinter(param, args);
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
