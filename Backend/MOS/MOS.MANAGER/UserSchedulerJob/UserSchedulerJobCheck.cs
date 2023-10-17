using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.UserSchedulerJob
{
    class UserSchedulerJobCheck : BusinessBase
    {
        public UserSchedulerJobCheck()
            :base()
        {

        }
        public UserSchedulerJobCheck(Inventec.Core.CommonParam param)
            :base(param)
	    {
                
	    }

        internal bool ExistsJob(string jobName, ref UserSchedulerJobSDO userSchedulerJobSDO)
        {
            bool valid = false;
            try
            {
                if (String.IsNullOrEmpty(jobName))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }
                string query = "SELECT * FROM USER_SCHEDULER_JOBS WHERE JOB_NAME = :param0 ";
                List<UserSchedulerJobSDO> userSchedulerJobSDOs = DAOWorker.SqlDAO.GetSql<UserSchedulerJobSDO>(query, jobName);
                if (userSchedulerJobSDOs != null && userSchedulerJobSDOs.Count > 0)
                {
                    userSchedulerJobSDO = userSchedulerJobSDOs.First();
                    valid = true;
                }
                else
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
