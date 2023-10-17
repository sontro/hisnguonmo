using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.HisRadixChangeCabinetList.HisRadixChangeCabinetList
{
    class HisRadixChangeCabinetBehavior : Tool<IDesktopToolContext>, IHisRadixChangeCabinetList
    {
        object[] entity;
        internal HisRadixChangeCabinetBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }
    
        object IHisRadixChangeCabinetList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                    }
                }
                if (moduleData != null)
                {
                    return new UCHisRadixChangeCabinet(moduleData);
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
