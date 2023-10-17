using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PharmacyCashier.PharmacyCashier
{
    class PharmacyCashierBehavior : Tool<IDesktopToolContext>, IPharmacyCashier
    {
        Inventec.Desktop.Common.Modules.Module Module;

        internal PharmacyCashierBehavior()
            : base()
        {

        }
        internal PharmacyCashierBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object IPharmacyCashier.Run()
        {
            object result = null;
            try
            {
                result = new frmPharmacyCashier(Module);
                if (result == null) throw new NullReferenceException(LogUtil.TraceData("Module", Module));
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
