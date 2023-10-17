using HIS.Desktop.Plugins.AssignNutrition.AssignNutrition;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignNutrition
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.AssignNutrition",
       "Chỉ định dinh dưỡng",
       "Common",
       14,
       "newitem_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class AssignNutritionProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public AssignNutritionProcessor()
        {
            param = new CommonParam();
        }
        public AssignNutritionProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IAssignNutrition behavior = AssignNutritionFactory.MakeIAssignNutrition(param, args);
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
