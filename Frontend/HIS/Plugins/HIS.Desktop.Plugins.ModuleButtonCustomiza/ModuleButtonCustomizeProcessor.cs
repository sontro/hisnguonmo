using HIS.Desktop.Plugins.ModuleButtonCustomize.ModuleButtonCustomize;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ModuleButtonCustomize
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ModuleButtonCustomize",
       "Thiết lập Tùy biến nút - Chức năng",
       "Common",
       0,
       "chon-phong.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class ModuleButtonCustomizeProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ModuleButtonCustomizeProcessor()
        {
            param = new CommonParam();
        }
        public ModuleButtonCustomizeProcessor(CommonParam paramBusiness)          
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IModuleButtonCustomize behavior = ModuleButtonCustomizeFactory.MakeIModuleButtonCustomize(param, args);
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
