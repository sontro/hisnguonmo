using Inventec.Common.Logging;
using ACS.LogManager;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.MANAGER.ActivityProcessor;

namespace ACS.QuartzScheduler.ActivityLog
{
    internal class ActivityLogJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                ActivityLogSave.Run();
                LogSystem.Info("ActivityLogJob thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
