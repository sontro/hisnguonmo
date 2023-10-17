using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisRoomTime
{
    class HisRoomTimeBehavior : BusinessBase, IHisRoomTime
    {
        object[] entity;
        internal HisRoomTimeBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisRoomTime.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_EXECUTE_ROOM executeRoom = null;

                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is V_HIS_EXECUTE_ROOM)
                            {
                                executeRoom = (V_HIS_EXECUTE_ROOM)entity[i];
                            }
                        }
                    }
                }

                if (executeRoom != null)
                {
                    return new frmHisRoomTime(moduleData, executeRoom);
                }
                else
                {
                    return new frmHisRoomTime(moduleData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
