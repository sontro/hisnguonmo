using HIS.Desktop.Plugins.ExroRoom.ExroRoom;
using Inventec.Core;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HIS.Desktop.Plugins.ExroRoom
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
    "HIS.Desktop.Plugins.ExroRoom",
    "Cấu hình phòng - phòng",
    "Common",
    62,
    "technology_32x32.png",
    "A",
    Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
    true,
    true)]
    public class ExroRoomProcessor : ModuleBase, IDesktopRoot
    {
        private CommonParam param;
        public ExroRoomProcessor()
        {
            param = new CommonParam();
        }
        public ExroRoomProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            var result = (object )null;
            try
            {
                var behavior = ExroRoomFactory.MakeIMedicineTypeRoom(param, args);
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
