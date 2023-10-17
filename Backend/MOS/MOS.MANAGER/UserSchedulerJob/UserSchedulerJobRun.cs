using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.UserSchedulerJob
{
    class UserSchedulerJobRun : BusinessBase
    {
        public UserSchedulerJobRun()
            :base()
        {

        }
        public UserSchedulerJobRun(Inventec.Core.CommonParam param)
            :base(param)
	    {
                
	    }

        internal bool Run(string data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(data);
                if (!valid)
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                if (valid)
                {
                    string query = "BEGIN DBMS_SCHEDULER.RUN_JOB(job_name => :param0, USE_CURRENT_SESSION => FALSE); END;";
                    string jobName = String.Format("HIS_RS.{0}",data);
                    result = DAOWorker.SqlDAO.Execute(query, jobName);
                    if (!result)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.UserSchedulerJob_KhoiChayTienTrinh_ThatBai, data);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
