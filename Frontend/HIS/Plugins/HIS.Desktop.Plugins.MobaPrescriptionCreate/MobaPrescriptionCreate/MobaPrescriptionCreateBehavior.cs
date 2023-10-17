using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaPrescriptionCreate.MobaPrescriptionCreate
{
    class MobaPrescriptionCreateBehavior : Tool<IDesktopToolContext>, IMobaPrescriptionCreate
    {
       long expMestId;
        Inventec.Desktop.Common.Modules.Module Module;
        internal MobaPrescriptionCreateBehavior()
            : base()
        {

        }

        internal MobaPrescriptionCreateBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, long data)
            : base()
        {
            Module = moduleData;
            expMestId = data;
        }

        object IMobaPrescriptionCreate.Run()
        {
            object result = null;
            try
            {
                result = new frmMobaPrescriptionCreate(Module, expMestId);
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
