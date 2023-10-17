using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentLockList.TreatmentLockList
{
    class TreatmentLockListBehavior : Tool<IDesktopToolContext>, ITreatmentLockList
    {
        long entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;

        internal TreatmentLockListBehavior()
            : base()
        {

        }

        internal TreatmentLockListBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data)
            : base()
        {
            entity = data;
            moduleData = module;
        }

        object ITreatmentLockList.Run()
        {
            object result = null;
            try
            {
                result = new frmTreatmentLockList(moduleData, entity);
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
