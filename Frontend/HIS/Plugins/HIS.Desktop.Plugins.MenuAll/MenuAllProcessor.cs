using HIS.Desktop.Plugins.MenuAll.MenuAll;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MenuAll
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
      "HIS.Desktop.Plugins.MenuAll",
      "Tất cả",
      "Common",
      34,
      "contentarrangeinrows_32x32.png",
      "A",
      Module.MODULE_TYPE_ID__FORM,
      true,
      true
      )
   ]
    public class MenuAllProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MenuAllProcessor()
        {
            param = new CommonParam();
        }
        public MenuAllProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IMenuAll behavior = MenuAllFactory.MakeIMenuAll(param, args);
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
