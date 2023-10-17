using HIS.Desktop.Plugins.ExpMestVitaminA.ExpMestVitaminA;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestVitaminA
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ExpMestVitaminA",
       "Xuất Vitamin A",
       "Common",
       25,
       "vitaminA.jpg",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class ExpMestVitaminAProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestVitaminAProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestVitaminAProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IExpMestVitaminA behavior = ExpMestVitaminAFactory.MakeIExpMestVitaminA(param, args);
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
