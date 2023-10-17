using HIS.Desktop.Plugins.LisDeliveryNoteDetail.LisDeliveryNoteDetail;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisDeliveryNoteDetail
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.LisDeliveryNoteDetail",
        "Chi tiết phiếu giao nhận bệnh phẩm",
        "Common",
        16,
        "mau-benh-pham.png",
        "E",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)
    ]
    public class LisDeliveryNoteDetailProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LisDeliveryNoteDetailProcessor()
        {
            param = new CommonParam();
        }
        public LisDeliveryNoteDetailProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ILisDeliveryNoteDetail behavior = LisDeliveryNoteDetailFactory.MakeIControl(param, args);
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
            return true;
        }
    }
}
