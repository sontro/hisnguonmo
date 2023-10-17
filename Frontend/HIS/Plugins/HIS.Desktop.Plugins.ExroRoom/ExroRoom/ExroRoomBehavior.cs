using System;
using System.Collections.Generic;
using System.Linq;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ExroRoom.ExroRoom
{
    internal class ExroRoomBehavior : Tool<IDesktopToolContext>, IExroRoom
    {
        private object[] entity;
        private V_HIS_EXECUTE_ROOM executeRoom;
        private MOS.EFMODEL.DataModels.V_HIS_ROOM executeRoom1;
        private V_HIS_RECEPTION_ROOM receptionRoom;




        internal ExroRoomBehavior()
            : base()
        {
        }

        internal ExroRoomBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }


        object IExroRoom.Run()
        {
            object result = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is V_HIS_EXECUTE_ROOM)
                        {
                            executeRoom = (V_HIS_EXECUTE_ROOM)item;
                        }
                        if (item is V_HIS_ROOM)
                        {
                            executeRoom1 = (V_HIS_ROOM)item;
                        }
                        if (item is V_HIS_RECEPTION_ROOM)
                        {
                            receptionRoom = (V_HIS_RECEPTION_ROOM)item;
                        }
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }

                    if (executeRoom != null)
                    {
                        result = new UCExroRoom(executeRoom, moduleData);
                    }
                    else
                    {
                        if (executeRoom1 != null)
                        {
                            result = new UCExroRoom(executeRoom1, moduleData);
                        }
                        else
                        {
                            if (receptionRoom != null)
                            {
                                result = new UCExroRoom(receptionRoom, moduleData);
                            }
                            else
                            {
                                result = new UCExroRoom(moduleData);
                            }
                        }
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
