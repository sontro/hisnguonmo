using HIS.Desktop.Plugins.KioskInformation.GetKioskInformation;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KioskInformation
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
      "HIS.Desktop.Plugins.KioskInformation",
      "Tra cứu thông tin",
      "Common",
      14,
      "kiosk.png",
      "A",
      Module.MODULE_TYPE_ID__FORM,
      true,
      true
      )
   ]
   public class KioskInformationProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public KioskInformationProcessor()
        {
            param = new CommonParam();
        }
        public KioskInformationProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IKioskInformation behavior = KioskInformationFactory.MakeIKioskInformation(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        //public override bool isEnable()
        //{
        //    return true;
        //}
    }
}
