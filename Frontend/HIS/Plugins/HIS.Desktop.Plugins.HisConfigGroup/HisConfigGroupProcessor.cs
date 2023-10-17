using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;
using HIS.Desktop.Plugins.HisConfigGroup.HisConfigGroup;
namespace HIS.Desktop.Plugins.HisConfigGroup
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisConfigGroup",
        "Hệ thống",
        "Bussiness",
        19,
        "ngon-ngu.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class HisConfigGroupProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public HisConfigGroupProcessor()
        {
            param = new CommonParam();
        }
        public HisConfigGroupProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IHisConfigGroup behavior = HisConfigGroupFatory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
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
