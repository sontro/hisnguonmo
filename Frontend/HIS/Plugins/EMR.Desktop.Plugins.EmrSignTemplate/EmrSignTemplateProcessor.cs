using EMR.Desktop.Plugins.EmrSignTemplate.EmrSignTemplate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrSignTemplate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "EMR.Desktop.Plugins.EmrSignTemplate",
       "Mẫu thiết lập ký",
       "menu popup",
       62,
       "",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)]
    public class EmrSignTemplateProcessor : ModuleBase, IDesktopRoot
    {CommonParam param;
        public EmrSignTemplateProcessor()
        {
            param = new CommonParam();
        }
        public EmrSignTemplateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IEmrSignTemplate behavior = EmrSignTemplateFactory.MakeIEmrTreatmentList(param, args);
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
