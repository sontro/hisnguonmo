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
    public sealed class DispenseMedicineUpdateBehavior : Tool<IDesktopToolContext>, IDispenseMedicine
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        long dispenseId;

        public DispenseMedicineUpdateBehavior()
            : base()
        {
        }

        public DispenseMedicineUpdateBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData, long _dispenseId)
            : base()
        {
            this.moduleData = moduleData;
            this.dispenseId = _dispenseId;
        }

        object IDispenseMedicine.Run()
        {
            try
            {
                return new frmDispenseMedicine(this.moduleData, this.dispenseId);
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
