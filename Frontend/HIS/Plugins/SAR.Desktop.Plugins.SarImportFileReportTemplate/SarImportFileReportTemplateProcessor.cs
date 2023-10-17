using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Core;
using SAR.Desktop.Plugins.SarImportFileReportTemplate.SarImportFileReportTemplate;
using Inventec.Desktop.Common.Modules;

namespace SAR.Desktop.Plugins.SarImportFileReportTemplate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
     "SAR.Desktop.Plugins.SarImportFileReportTemplate",
     "Import",
     "Common",
     14,
     "bao-cao.png",
     "A",
     Module.MODULE_TYPE_ID__FORM,
     true,
     true
     )
  ]
    public class SarImportFileReportTemplateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SarImportFileReportTemplateProcessor()
        {
            param = new CommonParam();
        }
        public SarImportFileReportTemplateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IImportFileReportTemplate behavior = ImportFileReportTemplateFactory.MakeIHisImportBed(param, args);
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
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            return result;
        }
    }
}

