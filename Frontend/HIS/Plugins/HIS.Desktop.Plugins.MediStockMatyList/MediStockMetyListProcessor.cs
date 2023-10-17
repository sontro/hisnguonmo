
using HIS.Desktop.Plugins.MediStockMatyList.MediStockMatyList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockMatyList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.MediStockMatyList",
        "Chức năng Kho - Loại vật tư",
        "Common",
        68,
        "reading_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class MediStockMatyListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MediStockMatyListProcessor()
        {
            param = new CommonParam();
        }
        public MediStockMatyListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMediStockMatyList behavior = MediStockMatyListFactory.MakeIMediStockMatyList(param, args);
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
