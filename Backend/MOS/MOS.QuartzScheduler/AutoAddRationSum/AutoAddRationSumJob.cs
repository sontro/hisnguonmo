using Inventec.Common.Logging;
using MOS.MANAGER.HisServiceReq.Ration.AutoAddRationSum;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MOS.QuartzScheduler.AutoAddRationSum
{
    class AutoAddRationSumJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info(String.Format("Begin AutoAddRationSumJob. Thread: {0}; Time= {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                AutoAddRationSumProcessor.Run();
                LogSystem.Info(String.Format("End AutoAddRationSumJob. Thread: {0}; Time= {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
