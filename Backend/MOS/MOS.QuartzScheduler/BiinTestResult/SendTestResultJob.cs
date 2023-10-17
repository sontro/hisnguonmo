using Inventec.Common.Logging;
using MOS.MANAGER.HisServiceReq.Test.SendResultToBiin;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.BiinTestResult
{
    class SendTestResultJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("SendTestResultJob Bat dau.");
                SendResultToBiinProcessor.Run();
                LogSystem.Info("SendTestResultJob Ket thuc.");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
