using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.UserSchedulerJob
{
    class UserSchedulerJobLock : BusinessBase
    {
        public UserSchedulerJobLock()
            :base()
        {

        }
        public UserSchedulerJobLock(Inventec.Core.CommonParam param)
            :base(param)
	    {
                
	    }

        internal bool ChangeLock(string data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                UserSchedulerJobSDO userSchedulerJobSDO = null;
                UserSchedulerJobCheck checker = new UserSchedulerJobCheck(param);
                valid = valid && checker.ExistsJob(data, ref userSchedulerJobSDO);
                if (valid)
                {
                    if (userSchedulerJobSDO.ENABLED == "TRUE")
                    {
                        string query = "BEGIN DBMS_SCHEDULER.disable(name => :param0); END;";
                        string jobName = String.Format("HIS_RS.{0}", data);
                        result = DAOWorker.SqlDAO.Execute(query, jobName);
                        if (!result)
                        {
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.UserSchedulerJob_KhoaTienTrinh_ThatBai, data);
                        }
                    }
                    else
                    {
                        string query = "BEGIN DBMS_SCHEDULER.enable(name => :param0); END;";
                        string jobName = String.Format("HIS_RS.{0}", data);
                        result = DAOWorker.SqlDAO.Execute(query, jobName);
                        if (!result)
                        {
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.UserSchedulerJob_MoKhoaTienTrinh_ThatBai, data);
                        }
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
