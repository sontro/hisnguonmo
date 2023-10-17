using HIS.Desktop.Plugins.MedicineSaleBill.MedicineSaleBill;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineSaleBill
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.MedicineSaleBill",
        "Thanh toán phiếu xuất bán thuốc",
        "Common",
        27,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    class MedicineSaleBillProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MedicineSaleBillProcessor()
        {
            param = new CommonParam();
        }
        public MedicineSaleBillProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMedicineSaleBill behavior = MedicineSaleBillFactory.MakeIMedicineSaleBill(param, args);
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
