using HIS.Desktop.Plugins.CallPatientNumOrder.CallPatientNumOrder;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientNumOrderNumOrder
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.CallPatientNumOrder",
       "Gọi bênh nhân stt",
       "Common",
       14,
       "CallPatient_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class CallPatientNumOrderProsessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public CallPatientNumOrderProsessor()
        {
            param = new CommonParam();
        }
        public CallPatientNumOrderProsessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ICallPatientNumOrder behavior = CallPatientNumOrderFactory.MakeICallPatientNumOrder(param, args);
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
