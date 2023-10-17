using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaSaleCreate.MobaSaleCreate
{
    class MobaSaleCreateBehavior : Tool<IDesktopToolContext>, IMobaSaleCreate
    {
       long expMestId;
        Inventec.Desktop.Common.Modules.Module Module;
        internal MobaSaleCreateBehavior()
            : base()
        {

        }

        internal MobaSaleCreateBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, long data)
            : base()
        {
            Module = moduleData;
            expMestId = data;
        }

        object IMobaSaleCreate.Run()
        {
            object result = null;
            try
            {
                result = new frmMobaSaleCreate(Module, expMestId);
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
