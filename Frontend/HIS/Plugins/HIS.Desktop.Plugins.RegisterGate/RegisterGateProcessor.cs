using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterGate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
     "HIS.Desktop.Plugins.RegisterGate",
     "Danh mục",
     "Bussiness",
     7,
     "showproduct_32x32.png",
     "A",
     Module.MODULE_TYPE_ID__FORM,
     true,
     true)
    ]
    public class RegisterGateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RegisterGateProcessor()
        {
            param = new CommonParam();
        }
        public RegisterGateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                DelegateSelectData delegateSelect = null;

                if (args.GetType() == typeof(object[]))
                {
                    if (args != null && args.Count() > 0)
                    {
                        for (int i = 0; i < args.Count(); i++)
                        {
                            if (args[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)args[i];
                            }
                            if (args[i] is DelegateSelectData)                            {
                                delegateSelect = (DelegateSelectData)args[i];
                            }
                           
                        }
                    }
                }

                if (delegateSelect == null)
                    result = new HIS.Desktop.Plugins.RegisterGate.RegisterGateForm.RegisterGateForm(moduleData);
                else
                    result = new HIS.Desktop.Plugins.RegisterGate.RegisterGateForm.RegisterGateForm(moduleData, delegateSelect);
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
