using Inventec.Core;
using Inventec.Desktop.Core;
using HIS.Desktop.Plugins.PublicServices_NT.PublicServices_NT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.LocalStorage.LocalData;

namespace HIS.Desktop.Plugins.PublicServices_NT
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.PublicServices_NT",
       "Công khai dịch vụ",
       "Common",
       23,
       "pivot_32x32.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class PublicServices_NTProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public PublicServices_NTProcessor()
        {
            param = new CommonParam();
        }
        public PublicServices_NTProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IPublicServices_NT behavior = PublicServices_NTFactory.MakeIPublicServices_NT(param, args);
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
