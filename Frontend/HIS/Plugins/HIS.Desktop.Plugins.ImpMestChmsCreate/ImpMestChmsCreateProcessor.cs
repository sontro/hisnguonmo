using HIS.Desktop.Plugins.ImpMestChmsCreate.ImpMestChmsCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestChmsCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ImpMestChmsCreate",
       "Tạo phiếu nhập",
       "Common",
       25,
       "mobaImp.jpg",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class ImpMestChmsCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImpMestChmsCreateProcessor()
        {
            param = new CommonParam();
        }
        public ImpMestChmsCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IImpMestChmsCreate behavior = ImpMestChmsCreateFactory.MakeIImpMestChmsCreate(param, args);
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
