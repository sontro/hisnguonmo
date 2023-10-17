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
    public sealed class PrepareUpdateBehavior : Tool<IDesktopToolContext>, IPrepare
    {
        HIS_PREPARE prepare;
        Inventec.Desktop.Common.Modules.Module Module;
        DelegateRefreshData refeshData;
        public PrepareUpdateBehavior()
            : base()
        {
        }

        public PrepareUpdateBehavior(CommonParam param, HIS_PREPARE _prepare, Inventec.Desktop.Common.Modules.Module module, DelegateRefreshData _refeshData)
            : base()
        {
            this.prepare = _prepare;
            this.Module = module;
            this.refeshData = _refeshData;
        }

        object IPrepare.Run()
        {
            try
            {
                return new frmPrepare(this.Module, prepare, this.refeshData);
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
