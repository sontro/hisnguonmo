using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ReportCountTreatment
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.ReportCountTreatment",
    "Thông tin ra, vào viện ngày",
    "Common",
    62,
    "",
    "A",
    Module.MODULE_TYPE_ID__FORM,
    true,
    true)]
    public class ReportCountTreatmentProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ReportCountTreatmentProcessor()
        {
            param = new CommonParam();
        }
        public ReportCountTreatmentProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ReportCountTreatment.IReportCountTreatment behavior = ReportCountTreatment.ReportCountTreatmentFactory.MakeIReportCountTreatment(param, args);
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
