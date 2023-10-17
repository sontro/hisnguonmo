using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BloodTransfusion.BloodTransfusion
{
    class BloodTransfusionBehavior : Tool<IDesktopToolContext>, IBloodTransfusion
    {
        string treatmentCode;
        Inventec.Desktop.Common.Modules.Module Module;

        internal BloodTransfusionBehavior(Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            Module = moduleData;
        }

        internal BloodTransfusionBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, string _treatmentCode)
            : base()
        {
            Module = moduleData;
            treatmentCode = _treatmentCode;
        }

        object IBloodTransfusion.Run()
        {
            object result = null;
            try
            {

                result = !string.IsNullOrEmpty(treatmentCode) ? new frmBloodTransfusion(Module, treatmentCode) : new frmBloodTransfusion(Module);
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
