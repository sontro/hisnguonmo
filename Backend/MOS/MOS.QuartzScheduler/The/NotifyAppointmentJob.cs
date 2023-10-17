using MOS.MANAGER.NmsNotification;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.The
{
    class NotifyAppointmentJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                Inventec.Core.CommonParam param = new Inventec.Core.CommonParam();
                NotifyAppointment notifyAppointment = new NotifyAppointment(param);
                var result = notifyAppointment.Run();
                if (result)
                {
                    Inventec.Common.Logging.LogSystem.Info("NotifyAppointmentJob thanh cong. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                }
                else
                {
                    Inventec.Common.Logging.LogSystem.Info("NotifyAppointmentJob that bai. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                }
                
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
