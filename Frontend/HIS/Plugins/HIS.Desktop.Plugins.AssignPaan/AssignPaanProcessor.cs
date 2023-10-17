using HIS.Desktop.Plugins.AssignPaan.AssignPaan;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPaan
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.AssignPaan",
       "Chỉ định thuốc - vật tư",
       "Common",
       14,
       "newitem_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class AssignPaanProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AssignPaanProcessor()
        {
            param = new CommonParam();
        }
        public AssignPaanProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IAssignPaan behavior = AssignPaanBehaviorFactory.MakeIAssignPaan(param, args);
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
