using Inventec.Common.Logging;
using MOS.MANAGER.HisTreatment.Util;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.AutoEndExamTreatment
{
    class AutoEndExamTreatmentJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info(String.Format("Begin AutoEndExamTreatment. Thread: {0}; Time= {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                HisTreatmentAutoEndExamProcessor.Run();
                LogSystem.Info(String.Format("End AutoEndExamTreatment. Thread: {0}; Time= {1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
