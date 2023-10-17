using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentBedRoomList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.TreatmentBedRoomList",
        "Thanh toán",
        "Common",
        59,
        "transactionBill.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TreatmentBedRoomListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TreatmentBedRoomListProcessor()
        {
            param = new CommonParam();
        }
        public TreatmentBedRoomListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITreatmentBedRoomList behavior = TreatmentBedRoomListFactory.MakeITreatmentBedRoomList(param, args);
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
