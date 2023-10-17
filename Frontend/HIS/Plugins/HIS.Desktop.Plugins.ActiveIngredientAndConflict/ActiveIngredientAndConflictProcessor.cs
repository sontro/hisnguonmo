using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ActiveIngredientAndConflict;
//using HIS.Desktop.Plugins.ActiveIngredientAndConflict.ActiveIngredientAndConflict;

namespace HIS.Desktop.Plugins.ActiveIngredientAndConflict
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ActiveIngredientAndConflict",
        "Thiết lập tương tác thuốc",
        "Common",
        62,
        "thuoc.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ActiveIngredientAndConflictProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ActiveIngredientAndConflictProcessor()
        {
            param = new CommonParam();
        }
        public ActiveIngredientAndConflictProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IActiveIngredientAndConflict behavior = ActiveIngredientAndConflictFactory.MakeIActiveIngredientAndConflict(param, args);
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
            return false;
        }
    }
}
