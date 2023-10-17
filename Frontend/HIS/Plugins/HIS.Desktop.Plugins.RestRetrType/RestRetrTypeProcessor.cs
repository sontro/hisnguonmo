using HIS.Desktop.Plugins.RestRetrType.RestRetrType;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.RestRetrType
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.RestRetrType",
        "Thiết lập kỹ thuật tập tương ứng với dịch vụ PHCN",
        "Common",
        62,
        "dich-vu.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class RestRetrTypeProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RestRetrTypeProcessor()
        {
            param = new CommonParam();
        }
        public RestRetrTypeProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRestRetrType behavior = RestRetrTypeFactory.MakeIRestRetrType(param, args);
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