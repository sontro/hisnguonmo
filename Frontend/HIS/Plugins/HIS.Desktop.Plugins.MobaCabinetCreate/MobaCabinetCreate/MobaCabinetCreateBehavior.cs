using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaCabinetCreate.MobaCabinetCreate
{
    class MobaCabinetCreateBehavior : Tool<IDesktopToolContext>, IMobaCabinetCreate
    {
       long expMestId;
        Inventec.Desktop.Common.Modules.Module Module;
        internal MobaCabinetCreateBehavior()
            : base()
        {

        }

        internal MobaCabinetCreateBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, long data)
            : base()
        {
            Module = moduleData;
            expMestId = data;
        }

        object IMobaCabinetCreate.Run()
        {
            object result = null;
            try
            {
                result = new frmMobaCabinetCreate(Module, expMestId);
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
