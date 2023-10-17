using HIS.Desktop.Plugins.MedicineVaccinBill.MedicineVaccinBill;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineVaccinBill
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.MedicineVaccinBill",
        "Thanh toán phiếu yêu cầu vắc xin",
        "Common",
        27,
        "expMestCreate.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    class MedicineVaccinBillProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public MedicineVaccinBillProcessor()
        {
            param = new CommonParam();
        }
        public MedicineVaccinBillProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IMedicineVaccinBill behavior = MedicineVaccinBillFactory.MakeIMedicineVaccinBill(param, args);
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
