using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.RoomMachine.RoomMachine
{
    class RoomMachineBehavior : Tool<IDesktopToolContext>, IRoomMachine
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal RoomMachineBehavior()
            : base()
        {

        }

        internal RoomMachineBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IRoomMachine.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                //    foreach (var item in entity)
                //    {
                //        if (item is long)
                //        {
                //            treatmentId = (long)item;
                //        }
                //        else if (item is Inventec.Desktop.Common.Modules.Module)
                //        {
                //            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                //        }
                //        if (currentModule != null && treatmentId > 0)
                //        {
                //            result = new UCRoomMachine(currentModule, treatmentId);
                //            break;
                //        }
                //    }
                    result = new UCRoomMachine();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
