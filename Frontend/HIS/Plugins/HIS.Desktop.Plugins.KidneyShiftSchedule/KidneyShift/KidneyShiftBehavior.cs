using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;

namespace HIS.Desktop.Plugins.KidneyShiftSchedule.KidneyShift
{
    public sealed class KidneyShiftBehavior : Tool<IDesktopToolContext>, IKidneyShift
    {
        Inventec.Desktop.Common.Modules.Module Module;
        KidneyShiftScheduleADO kidneyShiftScheduleADO;

        public KidneyShiftBehavior()
            : base()
        {
        }

        public KidneyShiftBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module module, KidneyShiftScheduleADO kidneyShiftScheduleADO)
            : base()
        {
            this.Module = module;
            this.kidneyShiftScheduleADO = kidneyShiftScheduleADO;
        }

        object IKidneyShift.Run()
        {
            try
            {
                return new UCKidneyShift(this.kidneyShiftScheduleADO, this.Module);
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
