using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.TreatmentLockFee;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentLockFee.TreatmentLockFee
{
    public sealed class TreatmentLockFeeBehavior : Tool<IDesktopToolContext>, ITreatmentLockFee
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        long treatmentId;
        public TreatmentLockFeeBehavior()
            : base()
        {
        }

        public TreatmentLockFeeBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module module, long data)
            : base()
        {
            moduleData = module;
            treatmentId = data;
        }

        object ITreatmentLockFee.Run()
        {
            object result = null;
            try
            {
                result = new frmTreatmentLockFee(moduleData, treatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
            return result;
        }
    }
}
