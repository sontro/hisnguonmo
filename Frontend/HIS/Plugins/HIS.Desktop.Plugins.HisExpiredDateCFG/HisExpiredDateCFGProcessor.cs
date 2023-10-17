using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.HisExpiredDateCFG.HisExpiredDateCFG;

namespace HIS.Desktop.Plugins.HisExpiredDateCFG
{
    class HisExpiredDateCFGProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.HisExpiredDateCFG",
           "Thiết lập cảnh báo hạn dùng theo tuổi thọ",
           "",
           1,
           "thiet-lap.png",
           "Thiết lập",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]
        public class HisExpiredDateCFGProcessorA : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public HisExpiredDateCFGProcessorA()
            {
                param = new CommonParam();
            }
            public HisExpiredDateCFGProcessorA(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    IHisExpiredDateCFG behavior = HisExpiredDateCFGFactory.MakeIControl(param, args);
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
