using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using HIS.Desktop.Plugins.MediReactCreate.MediReactCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Plugins.MediReactCreate.MediReactCreate;

namespace HIS.Desktop.Plugins.MediReactCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.MediReactCreate",
       "Tạo thông tin phản ứng thuốc",
       "Common",
       0,
       "support_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class MediReactCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MediReactCreateProcessor()
        {
            param = new CommonParam();
        }
        public MediReactCreateProcessor(CommonParam paramBusiness)          
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IMediReactCreate behavior = MediReactCreateFactory.MakeIMediReactCreate(param, args);
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
