using HIS.Desktop.Plugins.TrackingInMediRecord.TrackingInMediRecord;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TrackingInMediRecord
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.TrackingInMediRecord",
           "Danh sách tờ điều trị trong bệnh án",
           "popup",
           190,
           "DanhSachToDieuTriTrongBenhAn.png",
           "A",
           Module.MODULE_TYPE_ID__COMBO,
           true,
           true)]

    public class TrackingInMediRecordProcessor : ModuleBase, IDesktopRoot
    {
CommonParam param;
        public TrackingInMediRecordProcessor()
        {
            param = new CommonParam();
        }
        public TrackingInMediRecordProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITrackingInMediRecord behavior = TrackingInMediRecordFactory.MakeIApprovalExportPrescription(param, args);
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
