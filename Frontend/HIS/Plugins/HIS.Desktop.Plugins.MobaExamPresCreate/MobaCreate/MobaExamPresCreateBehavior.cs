using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaExamPresCreate.MobaCreate
{
    class MobaExamPresCreateBehavior : Tool<IDesktopToolContext>, IMobaExamPresCreate
    {
       long expMestId;
        Inventec.Desktop.Common.Modules.Module Module;
        internal MobaExamPresCreateBehavior()
            : base()
        {

        }

        internal MobaExamPresCreateBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, long data)
            : base()
        {
            Module = moduleData;
            expMestId = data;
        }

        object IMobaExamPresCreate.Run()
        {
            object result = null;
            try
            {
                result = new frmImpMobaCreate(Module, expMestId);
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
