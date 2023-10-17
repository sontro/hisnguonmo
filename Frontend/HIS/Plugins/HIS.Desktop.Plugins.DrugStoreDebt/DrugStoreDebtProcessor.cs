using HIS.Desktop.Plugins.DrugStoreDebt.DrugStoreDebt;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DrugStoreDebt
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.DrugStoreDebt",
        "Chốt nợ nhà thuốc",
        "Common",
        59,
        "thanh-toan.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class frmDrugStoreDebtProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public frmDrugStoreDebtProcessor()
        {
            param = new CommonParam();
        }
        public frmDrugStoreDebtProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IDrugStoreDebt behavior = DrugStoreDebtFactory.MakeIDrugStoreDebt(param, args);
                result = (behavior != null) ? behavior.Run() : null;
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
