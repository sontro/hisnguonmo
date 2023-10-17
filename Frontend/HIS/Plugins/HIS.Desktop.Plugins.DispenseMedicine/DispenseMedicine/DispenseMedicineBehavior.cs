using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.DispenseMedicine;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.DispenseMedicine.DispenseMedicine
{
    public sealed class DispenseMedicineBehavior : Tool<IDesktopToolContext>, IDispenseMedicine
    {
        Inventec.Desktop.Common.Modules.Module moduleData;

        public DispenseMedicineBehavior()
            : base()
        {
        }

        public DispenseMedicineBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
        }

        object IDispenseMedicine.Run()
        {
            try
            {
                return new frmDispenseMedicine(this.moduleData);
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
