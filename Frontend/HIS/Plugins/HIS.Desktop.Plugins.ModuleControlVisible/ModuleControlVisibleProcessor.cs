using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using HIS.Desktop.Plugins.ModuleControlVisible.ChooseRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Common.Modules;

namespace HIS.Desktop.Plugins.ModuleControlVisible
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.ModuleControlVisible",
       "Thiết lập Control ẩn - Chức năng",
       "Common",
       0,
       "chon-phong.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class ChooseRoomProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ChooseRoomProcessor()
        {
            param = new CommonParam();
        }
        public ChooseRoomProcessor(CommonParam paramBusiness)          
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IChooseRoom behavior = ChooseRoomFactory.MakeIChooseRoom(param, args);
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
