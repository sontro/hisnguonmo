using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalExportPrescription
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ApprovalExportPrescription",
        "Thực xuất thuốc",
        "Common",
        190,
        "xuat-kho.png",
        "A",
        Module.MODULE_TYPE_ID__COMBO,
        true,
        true)]

    public class ApprovalExportPrescriptionProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ApprovalExportPrescriptionProcessor()
        {
            param = new CommonParam();
        }
        public ApprovalExportPrescriptionProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ApprovalExportPrescription.IApprovalExportPrescription behavior = ApprovalExportPrescription.ApprovalExportPrescriptionFactory.MakeIApprovalExportPrescription(param, args);
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
