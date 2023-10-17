using Inventec.Core;
using HIS.Desktop.Common;
using HIS.Desktop.Plugins.CallPatientNumOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CallPatientNumOrder.CallPatientNumOrder
{
    public sealed class CallPatientNumOrderBehavior : Tool<IDesktopToolContext>, ICallPatientNumOrder
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module module;
        public CallPatientNumOrderBehavior()
            : base()
        {

        }

        public CallPatientNumOrderBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICallPatientNumOrder.Run()
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
