using Inventec.Common.Logging;
using Inventec.Token.ResourceSystem;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Credentials
{
    internal class ScanTimeOutJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                ResourceTokenManager.ScanTimeOutCredentials();
                LogSystem.Info("ScanTimeOutCredentialsJob thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
