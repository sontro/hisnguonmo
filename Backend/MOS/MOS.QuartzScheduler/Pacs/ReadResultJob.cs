using Inventec.Common.Logging;
using MOS.MANAGER.HisServiceReq.Pacs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.Pacs
{
    class ReadResultJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("ReadFileFromPacs bat dau");
                PacsSwitch.Reader();
                LogSystem.Info("ReadFileFromPacs ket thuc");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
