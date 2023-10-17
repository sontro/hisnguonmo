using Inventec.Common.Logging;
using MOS.MANAGER.Config;
using MOS.PubSub;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.PubSub
{
    class PubSubJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("PubSubJob bat dau");
                PubSubProcessor.ConnectPubSub(PubSubServerCFG.PUB_SUB_SERVER_INFO, PubSubServerCFG.Time_Check_Connection);
                LogSystem.Info("PubSubJob ket thuc");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
