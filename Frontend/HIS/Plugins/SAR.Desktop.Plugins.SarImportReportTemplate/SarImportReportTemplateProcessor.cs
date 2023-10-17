using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using SAR.Desktop.Plugins.SarImportReportTemplate.SarImportReportTemplate;

namespace SAR.Desktop.Plugins.SarImportReportTemplate
{
     [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "SAR.Desktop.Plugins.SarImportReportTemplate",
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
    public class SarImportReportTemplateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SarImportReportTemplateProcessor()
        {
            param = new CommonParam();
        }
        public SarImportReportTemplateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IImportReportTemplate behavior = ImportReportTemplateFactory.MakeIHisImportBed(param, args);
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

