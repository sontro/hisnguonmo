using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportRegisterByXml
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ImportRegisterByXml",
        "",
        "Common",
        600,
        "",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ImportRegisterByXmlProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImportRegisterByXmlProcessor()
        {
            param = new CommonParam();
        }

        public ImportRegisterByXmlProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ImportRegisterByXml.IImportRegisterByXml behavior = ImportRegisterByXml.ImportRegisterByXmlFactory.MakeIImportRegisterByXml(param, args);
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
