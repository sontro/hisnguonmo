using HIS.Desktop.Plugins.ImportRevenue.ImportRevenue;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportRevenue
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ImportRevenue",
       "Nhập Dữ Liệu Doanh Thu",
       "Common",
       54,
       "importRevenue.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)]
    public class ImportRevenueProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImportRevenueProcessor()
        {
            param = new CommonParam();
        }
        public ImportRevenueProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IImportRevenue behavior = ImportRevenueFactory.MakeIImportRevenue(param, null);
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
