using HIS.Desktop.Plugins.RegisterExamKiosk.WaitingScreen;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
      "HIS.Desktop.Plugins.RegisterExamKiosk",
      "Đăng ký khám",
      "Common",
      34,
      "kiosk.png",
      "A",
      Module.MODULE_TYPE_ID__FORM,
      true,
      true
      )
   ]
    public class WaitingScreenProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public WaitingScreenProcessor()
        {
            param = new CommonParam();
        }
        public WaitingScreenProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IWaitingScreen behavior = WaitingScreenFactory.MakeIWaitingScreen(param, args);
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
            return true;
        }
    }
}
