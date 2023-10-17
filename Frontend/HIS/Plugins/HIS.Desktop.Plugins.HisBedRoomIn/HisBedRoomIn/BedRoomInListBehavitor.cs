using HIS.Desktop.ADO;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBedRoomIn
{
    class BedRoomInListBehavitor : BusinessBase, IBedRoomInList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module Module;
        V_HIS_TREATMENT_BED_ROOM treatmentBedRoom;
        HIS.Desktop.Common.RefeshReference refreshRef;
        long treatmentID;

        internal BedRoomInListBehavitor()
            : base()
        {
        }

        public BedRoomInListBehavitor(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IBedRoomInList.Run()
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
                        else if (item is HIS.Desktop.Common.RefeshReference)
                        {
                            refreshRef = (HIS.Desktop.Common.RefeshReference)item;
                        }
                        else if (item is long)
                        {
                            treatmentID = (long)item;
                        }

                        if (Module != null && treatmentID > 0)
                        {
                            result = new FormHisBedRoomIn(Module, treatmentID, refreshRef);
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
