using Inventec.Common.Logging;
using Inventec.Token.ResourceSystem;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.QuartzScheduler.TokenUnAvailable
{
    internal class ScanTokenUnAvailableJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            try
            {
                if (DateTime.Now.Hour > 23 && DateTime.Now.Hour < 2)
                {
                    //Scan remove token timeout and credential data reference from acs DB
                    new ACS.MANAGER.AcsToken.AcsTokenManager().ScanToken();

                    LogSystem.Info("ScanTokenUnAvailable thanh cong. (De khong anh huong den hieu nang, job nay chi chay trong khoang tu (23h - 2h), chu ky 1 gio chay 1 lan de phong truong hop bi restart lien tuc dan den khong chay duoc job). Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
