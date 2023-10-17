
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using LIS.Desktop.Plugins.LisAntibioticRange.LisAntibioticRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisAntibioticRange
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "LIS.Desktop.Plugins.LisAntibioticRange",
        "",
        "",
        0,
        "",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class LisAntibioticRangeProcesor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LisAntibioticRangeProcesor()
        {
            param = new CommonParam();
        }
        public LisAntibioticRangeProcesor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ILisAntibioticRange behavior = LisAntibioticRangeFactory.MakeILisAntibioticRange(param, args);
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
