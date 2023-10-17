using Inventec.Common.Logging;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Lis
{
    class SendRequestJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info(String.Format("Begin SendToLisJob. Thread: {0}; Time= {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                LisSwitch.Sender();
                LogSystem.Info(String.Format("End SendToLisJob. Thread: {0}; Time= {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
