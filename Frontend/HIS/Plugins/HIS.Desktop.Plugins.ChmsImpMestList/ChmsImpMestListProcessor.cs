using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ChmsImpMestList.ChmsImpMestList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.ChmsImpMestList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ChmsImpMestList",
        "Danh sách nhập chuyển kho",
        "Common",
        62,
        "ChmsExpMest_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]

    public class ChmsImpMestListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ChmsImpMestListProcessor()
        {
            param = new CommonParam();
        }
        public ChmsImpMestListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IChmsImpMestList behavior = ChmsImpMestListFactory.MakeIChmsImpMestList(param, args);
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
