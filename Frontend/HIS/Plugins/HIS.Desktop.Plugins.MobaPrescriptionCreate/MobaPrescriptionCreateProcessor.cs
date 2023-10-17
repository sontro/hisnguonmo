using HIS.Desktop.Plugins.MobaPrescriptionCreate.MobaPrescriptionCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaPrescriptionCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.MobaPrescriptionCreate",
       "Nhập đơn nội trú trả lại",
       "Common",
       25,
       "mobaImp.jpg",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class MobaPrescriptionCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MobaPrescriptionCreateProcessor()
        {
            param = new CommonParam();
        }
        public MobaPrescriptionCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IMobaPrescriptionCreate behavior = MobaPrescriptionCreateFactory.MakeIMobaPrescriptionCreate(param, args);
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
