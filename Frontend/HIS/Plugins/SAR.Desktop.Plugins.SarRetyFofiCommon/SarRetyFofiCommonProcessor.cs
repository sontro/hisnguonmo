using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using SAR.Desktop.Plugins.SarRetyFofiCommon;

namespace SAR.Desktop.Plugins.SarRetyFofiCommon
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "SAR.Desktop.Plugins.SarRetyFofiCommon",
       "Danh mục",
       "Bussiness",
       4,
       "q",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    class SarRetyFofiCommonProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SarRetyFofiCommonProcessor()
        {
            param = new CommonParam();
        }
        
        public SarRetyFofiCommonProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ISarRetyFofiCommon behavior = SarRetyFofiCommonFactory.MakeIControl(param, args);
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
