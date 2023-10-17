using HIS.Desktop.Plugins.HisExamSchedule.HisExamSchedule;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExamSchedule
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisExamSchedule",
           "Lịch khám bác sỹ",
           "menu popup",
           4,
           "lich.png",
           "A",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]

    public class HisExamScheduleProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
            public HisExamScheduleProcessor()
            {
                param = new CommonParam();
            }
            public HisExamScheduleProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    IHisExamSchedule behavior = HisExamScheduleFactory.MakeIControl(param, args);
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
