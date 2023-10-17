using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBloodTypeVolume.HisBloodTypeVolume
{
    class HisBloodTypeVolumeBehavior : Tool<IDesktopToolContext>, IHisBloodTypeVolume
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;     
        internal HisBloodTypeVolumeBehavior()
            : base()
        {

        }

        internal HisBloodTypeVolumeBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IHisBloodTypeVolume.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;                
                      
                    }
                }

                if (moduleData != null)
                {
                    return new HIS.Desktop.Plugins.HisBloodTypeVolume.Run.frmBloodTypeVolume(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
