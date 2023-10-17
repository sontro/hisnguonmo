using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrTreatmentList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "EMR.Desktop.Plugins.EmrTreatmentList",
    "Hồ sơ bệnh án",
    "Common",
    62,
    "",
    "A",
    Module.MODULE_TYPE_ID__UC,
    true,
    true)]
    public class EmrTreatmentListProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public EmrTreatmentListProcessor()
        {
            param = new CommonParam();
        }
        public EmrTreatmentListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                EmrTreatmentList.IEmrTreatmentList behavior = EmrTreatmentList.EmrTreatmentListFactory.MakeIEmrTreatmentList(param, args);
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
