using HIS.Desktop.Plugins.ExportXml2076.ExportXml2076;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXml2076
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ExportXml2076",
       "Xuất xml CV2076",
       "Common",
       10,
       "bhyt.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]
    public class ExportXml2076Processor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExportXml2076Processor()
        {
            param = new CommonParam();
        }
        public ExportXml2076Processor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IExportXml2076 behavior = ExportXml2076Factory.MakeIExportXml2076(param, args);
                result = behavior != null ? (behavior.Run()) : null;
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
