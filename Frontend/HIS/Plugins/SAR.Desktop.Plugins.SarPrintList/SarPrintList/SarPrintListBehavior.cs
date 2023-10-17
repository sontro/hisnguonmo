using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;

namespace SAR.Desktop.Plugins.SarPrintList
{
    public sealed class SarPrintListBehavior : Tool<IDesktopToolContext>, ISarPrintList
    {
        string jsonPrintId;
        Inventec.Desktop.Common.Modules.Module Module;
        SarPrintADO sarPrintADO;

        public SarPrintListBehavior()
            : base()
        {
        }

        public SarPrintListBehavior(CommonParam param, SarPrintADO sarPrintADO, Inventec.Desktop.Common.Modules.Module module)
            : base()
        {
            this.sarPrintADO = sarPrintADO;
            this.Module = module;
        }

        object ISarPrintList.Run()
        {
            try
            {
                return new frmSarPrintList(this.Module, sarPrintADO);
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
