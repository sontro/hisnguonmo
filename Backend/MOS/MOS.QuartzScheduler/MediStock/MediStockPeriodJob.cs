using Inventec.Common.Logging;
using MOS.MANAGER.HisMediStockPeriod.AutoCreate;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.MediStock
{
    class MediStockPeriodJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("MediStockPeriodJob bat dau");
                new HisMediStockPeriodAutoCreate().Run();
                LogSystem.Info("MediStockPeriodJob ket thuc");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
