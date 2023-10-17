using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.AllocateExecuteRoom
{
    class UCAllocateExecuteRoomProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.AllocateExecuteRoom",
           "",
           "",
           0,
           "",
           "",
           Module.MODULE_TYPE_ID__UC,
           true,
           true)
        ]
        public class UCAllocateExecuteRoomQProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public UCAllocateExecuteRoomQProcessor()
            {
                param = new CommonParam();
            }
            public UCAllocateExecuteRoomQProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    AllocateExecuteRoom.IAllocateExecuteRoom behavior = AllocateExecuteRoom.AllocateExecuteRoomFactory.MakeIHisKskDriverList(param, args);
                    result = behavior != null ? (object)(behavior.Run()) : null;
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
                bool result = false;
                try
                {
                    result = true;
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                    result = false;
                }
                return result;
            }
        }
    }
}
