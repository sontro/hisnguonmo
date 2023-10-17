using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMR.Desktop.Plugins.EmrSignerFlow;
using EMR.Desktop.Plugins.EmrSignerFlow.EmrSignerFlow;

namespace EMR.Desktop.Plugins.EmrSignerFlow
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "EMR.Desktop.Plugins.EmrSignerFlow",
        "Thiết lập",
        "Common",
        62,
        "thiet_lap.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class EmrSignerFlowProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EmrSignerFlowProcessor()
        {
            param = new CommonParam();
        }
        public EmrSignerFlowProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IEmrSignerFlow behavior = EmrSignerFlowFactory.MakeIEmrSignerFlow(param, args);
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
