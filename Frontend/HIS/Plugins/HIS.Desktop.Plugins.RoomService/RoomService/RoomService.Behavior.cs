using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RoomService.RoomService
{
    class RoomServiceBehavior : Tool<IDesktopToolContext>, IRoomService
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;
        V_HIS_SERVICE service;
        MOS.EFMODEL.DataModels.V_HIS_ROOM executeRoom;

        internal RoomServiceBehavior()
            : base()
        {

        }

        internal RoomServiceBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IRoomService.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is V_HIS_SERVICE)
                        {
                            service = (V_HIS_SERVICE)item;
                        }
                        if (item is V_HIS_ROOM)
                        {
                            executeRoom = (V_HIS_ROOM)item;
                        }
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }
                    if (service != null)
                    {
                        result = new UCRoomService(service, currentModule);
                    }
                    else if (executeRoom != null)
                    {
                        result = new UCRoomService(executeRoom, currentModule);
                    }
                    else
                    {
                        result = new UCRoomService(currentModule);
                    }
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
