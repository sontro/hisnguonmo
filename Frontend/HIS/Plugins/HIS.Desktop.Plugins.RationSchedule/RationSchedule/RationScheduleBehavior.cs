using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.Common;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.RationSchedule.RationSchedule
{
    public sealed class RationScheduleBehavior:Tool<IDesktopToolContext>,IRationSchedule
    {
        object[] entity;

        public RationScheduleBehavior() 
            :base() 
        { 
        
        }

        public RationScheduleBehavior(CommonParam param, object[] filter) 
            :base() 
        {
            this.entity = filter;
        }

        object IRationSchedule.Run() {
            try { 
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                L_HIS_TREATMENT_BED_ROOM data = null;
                if ( entity.GetType() == typeof(object[]) ){
                    if (entity != null && entity.Count() > 0) { 
                        for(int i = 0; i < entity.Count(); i++){
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is L_HIS_TREATMENT_BED_ROOM)
                            {
                                data = (L_HIS_TREATMENT_BED_ROOM)entity[i];
                            }
                        }
                    }
                }

                return new frmRationSchedule(moduleData, data);
            }
            catch (Exception ex) {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }
        }
    }
}
