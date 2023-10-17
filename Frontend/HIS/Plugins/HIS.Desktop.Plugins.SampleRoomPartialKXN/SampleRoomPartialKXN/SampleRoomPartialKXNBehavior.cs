using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SampleRoomPartialKXN.SampleRoomPartialKXN
{
    class SampleRoomPartialKXNBehavior : Tool<IDesktopToolContext>, ISampleRoomPartialKXN
    {
        object[] entity;

        internal SampleRoomPartialKXNBehavior()
            : base()
        {

        }

        internal SampleRoomPartialKXNBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ISampleRoomPartialKXN.Run()
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
                if (moduleData != null)
                {
                    return new UCSampleRoomPartialKXN(moduleData.RoomTypeId, moduleData.RoomId);
                }
                else
                {
                    return null;
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
