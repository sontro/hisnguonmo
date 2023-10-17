using Inventec.Common.Logging;
using MOS.MANAGER.License;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.QuartzScheduler.License
{
    internal class LicenseJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                LogSystem.Info("LicenseJob bắt đầu.");
                LicenseProcessor.CheckLicense();
                LogSystem.Info("LicenseJob kết thúc.");
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
