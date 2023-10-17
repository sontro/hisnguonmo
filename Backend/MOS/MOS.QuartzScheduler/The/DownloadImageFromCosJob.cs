using Inventec.Common.Logging;
using MOS.MANAGER.HisPatient;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.The
{
    class DownloadImageFromCosJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                new HisPatientUpdateImageByHisCard().Run();
                LogSystem.Info("DownloadImageFromCos thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
