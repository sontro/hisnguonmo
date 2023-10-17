using HIS.Desktop.Plugins.MobaExamPresCreate.MobaCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaExamPresCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.MobaExamPresCreate",
       "Nhập đơn phòng khám trả lại",
       "Common",
       25,
       "mobaImp.jpg",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class MobaExamPresCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MobaExamPresCreateProcessor()
        {
            param = new CommonParam();
        }
        public MobaExamPresCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IMobaExamPresCreate behavior = MobaExamPresCreateFactory.MakeIMobaExamPresCreate(param, args);
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
