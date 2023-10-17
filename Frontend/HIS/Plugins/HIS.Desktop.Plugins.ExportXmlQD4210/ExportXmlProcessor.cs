using HIS.Desktop.Plugins.ExportXmlQD4210.ExportXml;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD4210
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ExportXmlQD4210",
       "Xuất xml QĐ4210",
       "Common",
       10,
       "exportXml.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]
    public class ExportXmlProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExportXmlProcessor()
        {
            param = new CommonParam();
        }
        public ExportXmlProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IExportXml behavior = ExportXmlFactory.MakeIExportXml(param, args);
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
