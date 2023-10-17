using HIS.Desktop.Plugins.GenerateRegisterOrder.GenerateRegisterOrder;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.GenerateRegisterOrder
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
      "HIS.Desktop.Plugins.GenerateRegisterOrder",
      "Đăng ký khám",
      "Common",
      34,
      "kiosk.png",
      "A",
      Module.MODULE_TYPE_ID__FORM,
      true,
      true
      )
   ]
    public class GenerateRegisterOrderProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public GenerateRegisterOrderProcessor()
        {
            param = new CommonParam();
        }
        public GenerateRegisterOrderProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IGenerateRegisterOrder behavior = GenerateRegisterOrderFactory.MakeIGenerateRegisterOrder(param, args);
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
