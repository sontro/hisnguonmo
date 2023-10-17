using Inventec.Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.SystemConfig
{
    internal class RefreshJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                new MOS.MANAGER.HisConfig.HisConfigManager().ResetAll();
                LogSystem.Info("RefreshSystemConfigJob thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
