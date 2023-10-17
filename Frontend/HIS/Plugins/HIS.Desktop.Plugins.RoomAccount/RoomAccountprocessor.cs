using HIS.Desktop.Plugins.RoomAccount.RoomAccount;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Common.Modules;
using Inventec.Core;

namespace HIS.Desktop.Plugins.RoomAccount
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.RoomAccount",
        "Thiết lập tài khoản phòng",
        "Common",
        62,
        "tai-khoan.png",
        "A",
        Inventec.Desktop.Common.Modules.Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class RoomAccountProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public RoomAccountProcessor()
        {
            param = new CommonParam();
        }
        public RoomAccountProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IRoomAccount behavior = RoomAccountFactory.MakeIRoomAccount(param, args);
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