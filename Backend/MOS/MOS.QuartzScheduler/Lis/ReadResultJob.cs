using Inventec.Common.Logging;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisServiceReq.Test.LisIntegrateThread;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Lis
{
    class ReadResultJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("ReadFileFromLis bat dau");
                LisSwitch.Reader();
                LogSystem.Info("ReadFileFromLis thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
