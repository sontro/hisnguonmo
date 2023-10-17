using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;

namespace HIS.Desktop.Plugins.HisHoldReturn.HoldReturn
{
    public sealed class HoldReturnBehavior : Tool<IDesktopToolContext>, IHoldReturn
    {
        Inventec.Desktop.Common.Modules.Module Module;
        HoldReturnADO currentHoldReturnADO;

        public HoldReturnBehavior()
            : base()
        {
        }

        public HoldReturnBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module module, HoldReturnADO holdReturnADO)
            : base()
        {
            this.Module = module;
            this.currentHoldReturnADO = holdReturnADO;
        }

        object IHoldReturn.Run()
        {
            try
            {
                return new UCHoldReturn(this.Module, this.currentHoldReturnADO);
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
