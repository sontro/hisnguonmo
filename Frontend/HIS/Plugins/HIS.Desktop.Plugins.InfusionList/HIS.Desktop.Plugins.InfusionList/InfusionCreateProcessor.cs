using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using HIS.Desktop.Plugins.InfusionCreate.InfusionCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Plugins.InfusionCreate.InfusionCreate;

namespace HIS.Desktop.Plugins.InfusionCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.InfusionCreate",
       "Tạo thông tin truyền dịch",
       "Common",
       0,
       "support_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class InfusionCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InfusionCreateProcessor()
        {
            param = new CommonParam();
        }
        public InfusionCreateProcessor(CommonParam paramBusiness)          
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IInfusionCreate behavior = InfusionCreateFactory.MakeIInfusionCreate(param, args);
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
