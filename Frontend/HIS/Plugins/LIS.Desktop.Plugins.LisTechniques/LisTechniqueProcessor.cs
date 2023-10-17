using Inventec.Core;
using Inventec.Desktop.Core;
using LIS.Desktop.Plugins.LisTechniques.LisTechniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisTechniques
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "LIS.Desktop.Plugins.LisTechniques",
    "Danh mục",
    "Bussiness",
    8,
    "",
    "A",
    Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__FORM,
    true,
    true)
        ]
    class LisTechniqueProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LisTechniqueProcessor()
        {
            param = new CommonParam();
        }
        public LisTechniqueProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }
        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ILisTechnique behavior = LisTechniqueFactory.MakeIcontrol(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
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
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
