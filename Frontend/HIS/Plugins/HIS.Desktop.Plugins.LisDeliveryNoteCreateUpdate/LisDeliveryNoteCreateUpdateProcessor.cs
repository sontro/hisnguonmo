using HIS.Desktop.Plugins.LisDeliveryNoteCreateUpdate.LisDeliveryNoteCreateUpdate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.LisDeliveryNoteCreateUpdate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.LisDeliveryNoteCreateUpdate",
        "LisDeliveryNoteCreateUpdate",
        "Menu",
        16,
        "mau-benh-pham.png",
        "E",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)
    ]
    public class LisDeliveryNoteCreateUpdateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LisDeliveryNoteCreateUpdateProcessor()
        {
            param = new CommonParam();
        }
        public LisDeliveryNoteCreateUpdateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ILisDeliveryNoteCreateUpdate behavior = LisDeliveryNoteCreateUpdateFactory.MakeILisDeliveryNoteCreateUpdate(param, args);
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
