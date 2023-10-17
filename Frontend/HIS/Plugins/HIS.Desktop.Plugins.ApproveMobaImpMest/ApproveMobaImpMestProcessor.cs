using HIS.Desktop.Plugins.ApproveMobaImpMest.ApproveMobaImpMest;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApproveMobaImpMest
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.ApproveMobaImpMest",
          "Duyệt phiếu nhập trả lại",
          "Common",
          23,
          "bidList.png",
          "A",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true
          )
       ]
    public class ApproveMobaImpMestProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ApproveMobaImpMestProcessor()
        {
            param = new CommonParam();
        }
        public ApproveMobaImpMestProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IApproveMobaImpMest behavior = ApproveMobaImpMestFactory.MakeIApproveMobaImpMest(param, args);
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
