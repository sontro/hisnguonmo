using Inventec.Common.Logging;
using MOS.MANAGER.HisTreatment.Util;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Emr
{
    class UploadEmrJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("Begin UploadEmrJob.");
                new HisTreatmentUploadEmr().Resync();
                LogSystem.Info("End UploadEmrJob.");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
