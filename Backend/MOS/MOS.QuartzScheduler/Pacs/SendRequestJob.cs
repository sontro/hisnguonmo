using Inventec.Common.Logging;
using MOS.MANAGER.HisServiceReq.Pacs;
using MOS.MANAGER.HisServiceReq.Pacs.PacsThread;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Pacs
{
    class SendRequestJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("SendToPacsJob Bat dau. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                PacsThreadSender.Run();
                LogSystem.Info("SendToPacsJob Ket thuc. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
