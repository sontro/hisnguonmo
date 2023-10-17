using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAssRoomSampleRoom.HisAssRoomSampleRoom
{
    class HisAssRoomSampleRoomBehavior : Tool<IDesktopToolContext>, IHisAssRoomSampleRoom
    {
        object[] entity;        
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal HisAssRoomSampleRoomBehavior()
            : base()
        {

        }

        internal HisAssRoomSampleRoomBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IHisAssRoomSampleRoom.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                    }
                }
                if (entity != null && entity.Count() > 0)
                {
                    result = new UC_HisAssRoomSampleRoom(moduleData);
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
