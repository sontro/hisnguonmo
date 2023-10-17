using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFundInfo
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.TreatmentFundInfo",
           "Kết thúc điều trị",
           "Common",
           16,
           "",
           "",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
       ]
    public class TreatmentFundInfoProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TreatmentFundInfoProcessor()
        {
            param = new CommonParam();
        }

        public TreatmentFundInfoProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            Inventec.Common.Logging.LogSystem.Error("TreatmentFinish begin");
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                TreatmentFundInfo.ITreatmentFundInfo behavior = TreatmentFundInfo.TreatmentFundInfoFactory.MakeITreatmentFundInfo(param, args);
                result = behavior != null ? (behavior.Run()) : null;
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
