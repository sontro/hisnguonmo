using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaDepaCreate.MobaDepaCreate
{
    class MobaDepaCreateBehavior : Tool<IDesktopToolContext>, IMobaDepaCreate
    {
       long expMestId;
        Inventec.Desktop.Common.Modules.Module Module;
        internal MobaDepaCreateBehavior()
            : base()
        {

        }

        internal MobaDepaCreateBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, long data)
            : base()
        {
            Module = moduleData;
            expMestId = data;
        }

        object IMobaDepaCreate.Run()
        {
            object result = null;
            try
            {
                result = new frmMobaDepaCreate(Module, expMestId);
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
