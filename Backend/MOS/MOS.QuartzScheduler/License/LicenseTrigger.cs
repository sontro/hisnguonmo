using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.License
{
    internal class LicenseTrigger
    {
        internal static void AddJob()
        {
            try
            {
                int configTime = 1200000;

                TimeSpan repeatTime = TimeSpan.FromMilliseconds(configTime);

                ISchedulerFactory schedFact = new StdSchedulerFactory();

                IScheduler sched = schedFact.GetScheduler();
                sched.Start();

                IJobDetail job = JobBuilder.Create<LicenseJob>().WithIdentity("CheckLicenseJob", "CheckLicense").Build();

                ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("CheckLicenseTrigger", "CheckLicense").StartNow().WithSimpleSchedule(x => x.WithInterval(repeatTime).RepeatForever()).Build();
                sched.ScheduleJob(job, trigger);

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
