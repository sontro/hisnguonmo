using HIS.Desktop.ADO;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisBedRoomOut
{
    class BedRoomOutListBehavitor : Tool<IDesktopToolContext>, IBedRoomOutList
    {
         object[] entity;
        Inventec.Desktop.Common.Modules.Module Module;
        L_HIS_TREATMENT_BED_ROOM treatmentBedRoom;
        long treatmentId;

        internal BedRoomOutListBehavitor()
            : base()
        {
        }

        public BedRoomOutListBehavitor(CommonParam param, object[] filter)
            : base()
        {
            entity = filter;
        }

        object IBedRoomOutList.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            Module = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                        else if (item is L_HIS_TREATMENT_BED_ROOM)
                        {
                            treatmentBedRoom = (L_HIS_TREATMENT_BED_ROOM)item;
                        }

                        if (Module != null && treatmentBedRoom != null)
                        {
                            result = new FormHisBedRoomOut(Module, treatmentBedRoom);
                            break;
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
