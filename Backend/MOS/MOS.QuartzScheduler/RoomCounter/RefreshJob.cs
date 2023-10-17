using Inventec.Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.RoomCounter
{
    class RefreshJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                MOS.MANAGER.Config.HisRoomCounterCFG.ResfeshCounter();
                LogSystem.Info("RefreshRoomCounterJob thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
