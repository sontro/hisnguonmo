using HIS.Desktop.Plugins.ListTestIndexMap.ListTestIndexMap;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.ListTestIndexMap
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ListTestIndexMap",
        "Ánh xạ chỉ số xét nghiệm",
        "Common",
        27,
        "anh-xa.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ListTestIndexMapProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ListTestIndexMapProcessor()
        {
            param = new CommonParam();
        }
        public ListTestIndexMapProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IListTestIndexMap behavior = ListTestIndexMapFactory.MakeIListTestIndexMap(param, args);
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