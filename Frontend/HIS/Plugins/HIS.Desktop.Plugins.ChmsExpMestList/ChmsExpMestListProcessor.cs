using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ChmsExpMestList.ChmsExpMestList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.ChmsExpMestList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ChmsExpMestList",
        "Danh sách xuất chuyển kho",
        "Common",
        62,
        "ChmsExpMest_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]

    public class ChmsExpMestListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ChmsExpMestListProcessor()
        {
            param = new CommonParam();
        }
        public ChmsExpMestListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IChmsExpMestList behavior = ChmsExpMestListFactory.MakeIChmsExpMestList(param, args);
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
