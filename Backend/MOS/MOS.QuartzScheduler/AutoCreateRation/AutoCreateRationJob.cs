using Inventec.Common.Logging;
using MOS.MANAGER.HisServiceReq.Ration.AutoAssign;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.AutoCreateRation
{
    class AutoCreateRationJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("Begin AutoCreateRationJob");
                HisServiceReqAutoAssignRationProcessor.Run();
                LogSystem.Info("End AutoCreateRationJob");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
