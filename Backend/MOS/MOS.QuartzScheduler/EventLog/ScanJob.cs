using Inventec.Common.Logging;
using MOS.LogManager;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.EventLog
{
    internal class ScanJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                EventLogSender.Run();
                LogSystem.Info("ScanEventLogJob thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
