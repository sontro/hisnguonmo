using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CareSlipList.CareSlipList
{
    class CareSlipListBehavitor : BusinessBase, ICareSlipList
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module Module;
        V_HIS_TREATMENT_BED_ROOM treatmentBedRoom;
        long treatmentId;

        internal CareSlipListBehavitor()
            : base()
        {
        }

        public CareSlipListBehavitor(CommonParam param, object[] filter)
            : base()
        {
           // this.treatmentId = treatmentId;
            //this.Module = module;
            entity = filter;
        }

        object ICareSlipList.Run()
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
                        else if (item is V_HIS_TREATMENT_BED_ROOM)
                        {
                            treatmentBedRoom = (V_HIS_TREATMENT_BED_ROOM)item;
                        }
                        else if (item is long)
                        {
                            treatmentId = (long)item;
                        }
                        if (Module != null && treatmentBedRoom != null)
                        {
                            result = new FormCareSlipList(Module, treatmentBedRoom);
                            break;
                        }
                        else if (Module != null && treatmentId > 0)
                        {
                            result = new FormCareSlipList(Module, treatmentId);
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
