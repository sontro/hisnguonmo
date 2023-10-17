using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.Common;

namespace HIS.Desktop.Plugins.HisTestSampleType.HisTestSampleType
{
    public sealed class HisTestSampleTypeBehavior:Tool<IDesktopToolContext>,IHisTestSampleType
    {
        object[] entity;

        public HisTestSampleTypeBehavior() 
            :base() 
        { 
        
        }

        public HisTestSampleTypeBehavior(CommonParam param, object[] filter) 
            :base() 
        {
            this.entity = filter;
        }

        object IHisTestSampleType.Run() {
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

                return new frmHisTestSampleType(moduleData);
            }
            catch (Exception ex) {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return null;
            }
        }
    }
}
