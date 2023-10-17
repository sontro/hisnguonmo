using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Remuneration.Remuneration
{
    class RemunerationBehavior : Tool<IDesktopToolContext>, IRemuneration
    {
        object[] entity;        
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal RemunerationBehavior()
            : base()
        {

        }

        internal RemunerationBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IRemuneration.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                if (entity != null && entity.Count() > 0)
                {
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
                    result = new UC_Remuneration(moduleData);
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
