using HTC.EFMODEL.DataModels;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocationIndrectFee.AllocationIndrectFee
{
    class AllocationIndrectFeeBehavior : Tool<IDesktopToolContext>, IAllocationIndrectFee
    {
        HTC_PERIOD htcPeriod = null;
        Inventec.Desktop.Common.Modules.Module Module;
        public AllocationIndrectFeeBehavior()
            : base()
        {
        }

        public AllocationIndrectFeeBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, HTC_PERIOD data)
            : base()
        {
            this.htcPeriod = data;
            this.Module = module;
        }

        object IAllocationIndrectFee.Run()
        {
            try
            {
                return new frmAllocationIndrectFee(this.Module, this.htcPeriod);
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
