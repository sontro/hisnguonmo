using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using HIS.Desktop.Plugins.HisAccidentBodyPart.HisAccidentBodyPart;

namespace HIS.Desktop.Plugins.HisAccidentBodyPart
{
    class HisAccidentBodyPartProcessors
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.HisAccidentBodyPart",
    "Danh mục",
    "Bussiness",
    4,
    "",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true)
 ]
        public class SarReportTemplateProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public SarReportTemplateProcessor()
            {
                param = new CommonParam();
            }
            public SarReportTemplateProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    IAccidentBodyPart behavior = AccidentBodyPartFactory.MakeIControl(param, args);
                    result = behavior != null ? (object)(behavior.Run()) : null;
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
}
