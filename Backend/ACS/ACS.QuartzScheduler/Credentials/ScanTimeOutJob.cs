using Inventec.Common.Logging;
using Inventec.Token.ResourceSystem;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.QuartzScheduler.Credentials
{
    internal class ScanTimeOutJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("ScanTimeOutCredentialsJob running. Hour=" + DateTime.Now.Hour);

                try
                {
                    ResourceTokenManager.ScanTimeOutCredentials();
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }

                if (DateTime.Now.Hour == 1)
                {
                    LogSystem.Info("ScanTimeOutCredentialsJob run in (1h) starting...");
                    //Scan remove token timeout from resource server (backend)

                    new ACS.MANAGER.AcsToken.AcsTokenManager().ScanToken();

                    LogSystem.Info("ScanTimeOutCredentialsJob run in (1h) finished. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                }

                LogSystem.Info("ScanTimeOutCredentialsJob Finish. Hour=" + DateTime.Now.Hour);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
