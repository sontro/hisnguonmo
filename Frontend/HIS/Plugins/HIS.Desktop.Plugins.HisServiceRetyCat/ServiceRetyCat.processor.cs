using HIS.Desktop.Plugins.HisServiceRetyCat.HisServiceRetyCat;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.HisServiceRetyCat
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.HisServiceRetyCat",
        "Thiết lập phòng dịch vụ",
        "Common",
        62,
        "thiet-lap.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class RoomServiceProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RoomServiceProcessor()
        {
            param = new CommonParam();
        }
        public RoomServiceProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRoomService behavior = RoomServiceFactory.MakeIRoomService(param, args);
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