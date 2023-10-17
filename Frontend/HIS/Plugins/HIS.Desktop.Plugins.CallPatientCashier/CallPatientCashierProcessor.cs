using HIS.Desktop.Plugins.CallPatientCashier.CallPatientCashier;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientCashier
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.CallPatientCashier",
       "Màn hình chờ thu ngân",
       "popup",
       14,
       "CallPatient_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class CallPatientCashierProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public CallPatientCashierProcessor()
        {
            param = new CommonParam();
        }
        public CallPatientCashierProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ICallPatientCashier behavior = CallPatientCashierFactory.MakeICallPatientNumOrder(param, args);
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
