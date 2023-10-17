using HTC.EFMODEL.DataModels;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RepartitionRatioCreate.RepartitionRatioCreate
{
    class RepartitionRatioCreateBehavior : Tool<IDesktopToolContext>, IRepartitionRatioCreate
    {
        Inventec.Desktop.Common.Modules.Module Module;
        HTC_PERIOD period = null;
        public RepartitionRatioCreateBehavior()
            : base()
        {
        }

        public RepartitionRatioCreateBehavior(Inventec.Desktop.Common.Modules.Module module, HTC_PERIOD data, CommonParam param)
            : base()
        {
            this.Module = module;
            this.period = data;
        }

        object IRepartitionRatioCreate.Run()
        {
            try
            {
                return new frmRepartitionRatioCreate(this.Module, period);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
