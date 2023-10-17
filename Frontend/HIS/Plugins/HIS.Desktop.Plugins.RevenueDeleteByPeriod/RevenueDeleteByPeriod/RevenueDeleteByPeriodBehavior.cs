using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RevenueDeleteByPeriod.RevenueDeleteByPeriod
{
    class RevenueDeleteByPeriodBehavior : Tool<IDesktopToolContext>, IRevenueDeleteByPeriod
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        internal RevenueDeleteByPeriodBehavior()
            : base()
        {

        }

        internal RevenueDeleteByPeriodBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.moduleData = module;
        }

        object IRevenueDeleteByPeriod.Run()
        {
            object result = null;
            try
            {

                result = new frmRevenueDeleteByPeriod(moduleData);
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => moduleData), moduleData));
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
