using Inventec.Common.Logging;
using MOS.MANAGER.HisSereServExt;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MOS.QuartzScheduler.AutoGetSereServAndUpdateGatherData
{
    class AutoGetSereServAndUpdateGatherDataJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info(String.Format("Begin AutoGetSereServAndUpdateGatherDataJob"));
                HisGetSereServAndUpdateGatherData.Run();
                LogSystem.Info(String.Format("End AutoGetSereServAndUpdateGatherDataJob"));
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
