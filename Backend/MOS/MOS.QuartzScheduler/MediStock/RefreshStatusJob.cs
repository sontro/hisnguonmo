using Inventec.Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.MediStock
{
    class RefreshStatusJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                MOS.MANAGER.Config.HisMediStockCFG.Reload();
                LogSystem.Info("RefreshMediStockStatusJob thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
