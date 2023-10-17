using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SaroExro.SaroExro
{
    class SaroExroBehavior : Tool<IDesktopToolContext>, ISaroExro
    {
        object[] entity;        
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal SaroExroBehavior()
            : base()
        {

        }

        internal SaroExroBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object ISaroExro.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
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
                    result = new UC_SaroExro(moduleData);
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
