using Inventec.Common.Logging;
using MOS.MANAGER.HisExpMest.Common.Auto;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.MediStock
{
    class AutoSetIsNotTakenJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("AutoSetIsNotTakenJob bat dau");
                new HisExpMestAutoSetIsNotTaken().Run();
                LogSystem.Info("AutoSetIsNotTakenJob ket thuc");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
