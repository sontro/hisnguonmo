using HIS.Desktop.Plugins.ExpMestOtherExport.ExpMestOtherExport;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestOtherExport
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpMestOtherExport",
        "Tạo xuất khác",
        "Common",
        27,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__COMBO,
        true,
        true)]
    class ExpMestOtherExportProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpMestOtherExportProcessor()
        {
            param = new CommonParam();
        }
        public ExpMestOtherExportProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpMestOtherExport behavior = ExpMestOtherExportFactory.MakeIExpMestOtherExport(param, args);
                result = (behavior != null) ? behavior.Run() : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
