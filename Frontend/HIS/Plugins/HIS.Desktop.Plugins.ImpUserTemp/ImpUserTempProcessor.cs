using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.Plugins.ImpUserTemp.ImpUserTemp;
namespace HIS.Desktop.Plugins.ImpUserTemp
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "HIS.Desktop.Plugins.ImpUserTemp",
          "Nội dung mẫu",
          "NDM",
          1,
          "nhom-nhan-vien.png",
          "A",
          Module.MODULE_TYPE_ID__FORM,
          true,
          true)
       ]
    public class ImpUserTempProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImpUserTempProcessor()
        {
            param = new CommonParam();
        }
        public ImpUserTempProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IImpUserTemp behavior = ImpUserTempFactory.MakeIControl(param, args);
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
