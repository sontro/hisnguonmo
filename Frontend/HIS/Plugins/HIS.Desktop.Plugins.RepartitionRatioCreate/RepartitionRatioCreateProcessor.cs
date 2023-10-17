using HIS.Desktop.Plugins.RepartitionRatioCreate.RepartitionRatioCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RepartitionRatioCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.RepartitionRatioCreate",
        "Tỷ lệ phân phối",
        "Common",
        52,
        "top_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class RepartitionRatioCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RepartitionRatioCreateProcessor()
        {
            param = new CommonParam();
        }
        public RepartitionRatioCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IRepartitionRatioCreate behavior = RepartitionRatioCreateFactory.MakeIRepartitionRatioCreate(param, args);
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
