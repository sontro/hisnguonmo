using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.LocalStorage.SdaConfig;
using HIS.Desktop.Plugins.RationSchedule.RationSchedule;

namespace HIS.Desktop.Plugins.RationSchedule
{
    
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.RationSchedule",
    "Báo ăn",
    "Common",
    14,
    "newcontact_32x32.png",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true
    )
    ]

    public class RationScheduleProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RationScheduleProcessor()
        {
            param = new CommonParam();
        }
        public RationScheduleProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IRationSchedule behavior = RationScheduleFactory.MakeIControl(param, args);
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
