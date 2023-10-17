using HIS.Desktop.Plugins.MobaImpMestCreate.MobaImpMestCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaImpMestCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.MobaImpMestCreate",
       "Tạo yêu cầu nhập thu hôi",
       "Common",
       25,
       "mobaImp.jpg",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class MobaImpMestCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MobaImpMestCreateProcessor()
        {
            param = new CommonParam();
        }
        public MobaImpMestCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IMobaImpMestCreate behavior = MobaImpMestCreateFactory.MakeIMobaImpMestCreate(param, args);
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
