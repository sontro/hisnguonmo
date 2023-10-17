using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.Common;

namespace HIS.Desktop.Plugins.HisRegimenHiv.RegimenHiv
{
    public sealed class RegimenHivBehavior:Tool<IDesktopToolContext>,IRegimenHiv
    {
        object[] entity;

        public RegimenHivBehavior() 
            :base() 
        { 
        
        }

        public RegimenHivBehavior(CommonParam param, object[] filter) 
            :base() 
        {
            this.entity = filter;
        }

        object IRegimenHiv.Run() {
            try { 
                Inventec.Desktop.Common.Modules.Module moduleData = null;

                if ( entity.GetType() == typeof(object[]) ){
                    if (entity != null && entity.Count() > 0) { 
                        for(int i = 0; i < entity.Count(); i++){
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                        }
                    }
                }

                return new frmRegimenHiv(moduleData);
            }
            catch (Exception ex) {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }
        }
    }
}
