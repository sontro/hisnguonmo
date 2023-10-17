using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.Prepare;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.Prepare.Prepare
{
    public sealed class PrepareBehavior : Tool<IDesktopToolContext>, IPrepare
    {
        HIS_TREATMENT treatment;
        Inventec.Desktop.Common.Modules.Module Module;
        public PrepareBehavior()
            : base()
        {
        }

        public PrepareBehavior(CommonParam param, HIS_TREATMENT _treatment, Inventec.Desktop.Common.Modules.Module module)
            : base()
        {
            this.treatment = _treatment;
            this.Module = module;
        }

        object IPrepare.Run()
        {
            try
            {
                return new frmPrepare(this.Module, treatment);
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
