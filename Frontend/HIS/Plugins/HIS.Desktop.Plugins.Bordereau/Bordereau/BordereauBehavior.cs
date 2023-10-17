using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.Bordereau;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Plugins.Bordereau.ADO;

namespace Inventec.Desktop.Plugins.Bordereau.Bordereau
{
    public sealed class BordereauBehavior : Tool<IDesktopToolContext>, IBordereau
    {
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module Module;
        public BordereauBehavior()
            : base()
        {
        }

        public BordereauBehavior(CommonParam param, long _treatmentId, Inventec.Desktop.Common.Modules.Module module)
            : base()
        {
            this.treatmentId = _treatmentId;
            this.Module = module;
        }

        object IBordereau.Run()
        {
            try
            {
                return new frmBordereau(this.Module, treatmentId);
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
