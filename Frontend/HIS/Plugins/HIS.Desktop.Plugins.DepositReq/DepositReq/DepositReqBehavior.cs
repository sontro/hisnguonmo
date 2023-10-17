using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;

namespace HIS.Desktop.Plugins.DepositReq.DepositReq
{
    public sealed class DepositReqBehavior : BusinessBase, IDepositReq
    {
        object[] entity;
       Inventec.Desktop.Common.Modules.Module Module;
       V_HIS_TREATMENT_BED_ROOM treatmentBedRoom;
       long treatmentID = 60559;

        internal DepositReqBehavior()
            : base()
        {
        }

        public DepositReqBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
            //this.Module = module;
            //entity = data;
        }

        object IDepositReq.Run()
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
                        /*
                        else if (item is long)
                        {
                            treatmentID = (long)item;
                        }*/
                        if (Module != null && treatmentID > 0)
                        {
                            result = new UC_DepositReq(Module, treatmentID);
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
