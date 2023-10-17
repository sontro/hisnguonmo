using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrSign
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "EMR.Desktop.Plugins.EmrSign",
    "Thiết lập ký",
    "Common",
    62,
    "",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true)]
    public class EmrSignProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EmrSignProcessor()
        {
            param = new CommonParam();
        }
        public EmrSignProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                EmrSign.IEmrSign behavior = EmrSign.EmrSignFactory.MakeIEmrSign(param, args);
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
            return false;
        }
    }
}
