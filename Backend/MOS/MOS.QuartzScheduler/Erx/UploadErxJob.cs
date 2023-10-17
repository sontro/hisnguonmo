using Inventec.Common.Logging;
using MOS.MANAGER.HisExpMest.Common.Auto;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Erx
{
    class UploadErxJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("Begin UploadErxJob.");
                new HisExpMestUploadErx().Run();
                LogSystem.Info("End UploadErxJob.");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
