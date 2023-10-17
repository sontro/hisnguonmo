using HIS.Desktop.Plugins.InfusionSumByTreatment.InfusionSumByTreatment;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfusionSumByTreatment
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.InfusionSumByTreatment",
        "Thanh toán",
        "Common",
        59,
        "InfusionSum.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class InfusionSumByTreatmentProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public InfusionSumByTreatmentProcessor()
        {
            param = new CommonParam();
        }
        public InfusionSumByTreatmentProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IInfusionSumByTreatment behavior = InfusionSumByTreatmentFactory.MakeIInfusionSumByTreatment(param, args);
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
