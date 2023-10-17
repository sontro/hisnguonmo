using Inventec.Common.Logging;
using MOS.MANAGER.HisServiceReq.Pacs;
using MOS.MANAGER.HisServiceReq.SmsSubclinicalResultNotifyThread;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.NotifySubclinicalResultSms
{
    class SendSmsJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("SendSmsJob Bat dau. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                SmsSubclinicalResultNotifyThreadSender.Run();
                LogSystem.Info("SendSmsJob Ket thuc. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
