using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.Plugins.InfomationExecute.InfomationExecute;
namespace HIS.Desktop.Plugins.InfomationExecute
{
      [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.InfomationExecute",
       "",
       "",
       0,
       "",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class InfomationExecuteProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InfomationExecuteProcessor()
        {
            param = new CommonParam();
        }
        public InfomationExecuteProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }
        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IInfomationExecute behavior = InfomationExecuteFactory.MakeIContentSubclinical(param, args);
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
