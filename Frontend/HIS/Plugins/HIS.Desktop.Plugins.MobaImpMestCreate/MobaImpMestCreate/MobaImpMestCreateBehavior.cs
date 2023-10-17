using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaImpMestCreate.MobaImpMestCreate
{
    class MobaImpMestCreateBehavior : Tool<IDesktopToolContext>, IMobaImpMestCreate
    {
       long expMestId;
        Inventec.Desktop.Common.Modules.Module Module;
        internal MobaImpMestCreateBehavior()
            : base()
        {

        }

        internal MobaImpMestCreateBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, long data)
            : base()
        {
            Module = moduleData;
            expMestId = data;
        }

        object IMobaImpMestCreate.Run()
        {
            object result = null;
            try
            {
                result = new frmMobaImpMestCreate(Module, expMestId);
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
