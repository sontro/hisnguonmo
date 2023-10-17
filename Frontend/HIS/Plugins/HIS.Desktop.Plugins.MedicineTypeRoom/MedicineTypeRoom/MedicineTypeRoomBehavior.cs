using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.MedicineTypeRoom.MedicineTypeRoom
{
    class MedicineTypeRoomBehavior : Tool<IDesktopToolContext>, IMedicineTypeRoom
    {
        object[] entity;
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module currentModule;
        V_HIS_EXECUTE_ROOM executeRoom;

        internal MedicineTypeRoomBehavior()
            : base()
        {

        }

        internal MedicineTypeRoomBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IMedicineTypeRoom.Run()
        {
            object result = null;
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
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }
                    if (executeRoom != null)
                    {
                        result = new UCMedicineTypeRoom(executeRoom, currentModule);
                    }
                    else
                    {
                        result = new UCMedicineTypeRoom(currentModule);
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
