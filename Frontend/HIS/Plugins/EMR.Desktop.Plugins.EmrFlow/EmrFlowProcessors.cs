using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using EMR.Desktop.Plugins.EmrFlow.EmrFlow;

namespace EMR.Desktop.Plugins.EmrFlow
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "EMR.Desktop.Plugins.EmrFlow",
        "Danh mục",
        "Bussiness",
        4,
        "",
        "D",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true
        )
    ]
    public class EmrFlowProcessors : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EmrFlowProcessors()
        {
            param = new CommonParam();
        }
        public EmrFlowProcessors(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IEmrFlow behavior = EmrFlowFactory.MakeIHisServicePatyList(param, args);
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
