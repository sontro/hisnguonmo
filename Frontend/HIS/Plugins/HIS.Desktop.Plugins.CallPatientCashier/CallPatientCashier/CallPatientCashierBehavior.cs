using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientCashier.CallPatientCashier
{
    public sealed class CallPatientCashierBehavior : Tool<IDesktopToolContext>, ICallPatientCashier
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module module;
        public CallPatientCashierBehavior()
            : base()
        {

        }

        public CallPatientCashierBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICallPatientCashier.Run()
        {
            try
            {
                foreach (var item in entity)
                {
                    if (item is Inventec.Desktop.Common.Modules.Module)
                    {
                        this.module = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (this.module != null)
                {
                    return new frmConfigForm(module);
                }
                return null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
