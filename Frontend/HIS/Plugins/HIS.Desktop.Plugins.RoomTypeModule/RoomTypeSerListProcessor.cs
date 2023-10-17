
using HIS.Desktop.Plugins.RoomTypeModule.RoomTypeSerList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RoomTypeModule
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.RoomTypeModule",
        "Danh sách loại phòng chức năng",
        "Common",
        68,
        "reading_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class RoomTypeSerListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RoomTypeSerListProcessor()
        {
            param = new CommonParam();
        }
        public RoomTypeSerListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRoomTypeSerList behavior = RoomTypeSerListFactory.MakeIRoomTypeSerList(param, args);
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
