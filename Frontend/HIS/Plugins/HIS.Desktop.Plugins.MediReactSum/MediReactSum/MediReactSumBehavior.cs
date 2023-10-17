using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediReactSum.MediReactSum
{
    class MediReactSumBehavior : Tool<IDesktopToolContext>, IMediReactSum
    {
        long treatmentId = 0;
        Inventec.Desktop.Common.Modules.Module Module;
        bool IsTreatmentList;
        internal MediReactSumBehavior()
            : base()
        {

        }

        internal MediReactSumBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data, bool isTreatmentList)
            : base()
        {
            this.Module = module;
            this.treatmentId = data;
            this.IsTreatmentList = isTreatmentList;
        }

        object IMediReactSum.Run()
        {
            object result = null;
            try
            {

                result = new frmMediReactSum(Module, treatmentId, IsTreatmentList);
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Module), Module));
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
