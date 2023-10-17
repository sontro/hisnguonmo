using HIS.Desktop.Plugins.MobaCabinetCreate.MobaCabinetCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaCabinetCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.MobaCabinetCreate",
       "Nhập đơn tủ trực trả lại",
       "Common",
       25,
       "mobaImp.jpg",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class MobaCabinetCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MobaCabinetCreateProcessor()
        {
            param = new CommonParam();
        }
        public MobaCabinetCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IMobaCabinetCreate behavior = MobaCabinetCreateFactory.MakeIMobaCabinetCreate(param, args);
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
