
using HIS.Desktop.Plugins.HisBranchTime.MediStockMetyList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBranchTime
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.MediStockMetyList",
        "Chức năng Kho - Loại thuốc",
        "Common",
        68,
        "thiet-lap.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class MediStockMetyListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MediStockMetyListProcessor()
        {
            param = new CommonParam();
        }
        public MediStockMetyListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMediStockMetyList behavior = MediStockMetyListFactory.MakeIMediStockMetyList(param, args);
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
