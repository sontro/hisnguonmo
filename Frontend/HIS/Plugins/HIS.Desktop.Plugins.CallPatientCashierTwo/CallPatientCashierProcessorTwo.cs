using HIS.Desktop.Plugins.CallPatientCashier.CallPatientCashierTwo;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientCashierTwo
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.CallPatientCashierTwo",
       "Màn hình chờ thu ngân 2",
       "popup",
       14,
       "CallPatient_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class CallPatientCashierProcessorTwo : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public CallPatientCashierProcessorTwo()
        {
            param = new CommonParam();
        }
        public CallPatientCashierProcessorTwo(CommonParam paramBusiness)
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
