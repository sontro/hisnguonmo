using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using ACS.SDO;
using ACS.UTILITY;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.UserSchedulerJob
{
    partial class UserSchedulerJobGetSql : GetBase
    {
        internal UserSchedulerJobGetSql()
            : base()
        {

        }

        internal UserSchedulerJobGetSql(Inventec.Core.CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<UserSchedulerJobResultSDO> Get(UserSchedulerJobFilter filter)
        {
            List<UserSchedulerJobResultSDO> userSchedulerJobResultSDOs = null;
            try
            {
                string query = "SELECT * FROM USER_SCHEDULER_JOBS WHERE 1 = 1 ";
                List<UserSchedulerJobSDO> userSchedulerJobSDOs = DAOWorker.SqlDAO.GetSql<UserSchedulerJobSDO>(query);
                if (userSchedulerJobSDOs != null && userSchedulerJobSDOs.Count > 0)
                {
                    userSchedulerJobResultSDOs = new List<UserSchedulerJobResultSDO>();
                    foreach (var item in userSchedulerJobSDOs)
                    {
                        UserSchedulerJobResultSDO userSchedulerJobResultSDO = this.MakeSchedulerJobResult(item);
                        userSchedulerJobResultSDOs.Add(userSchedulerJobResultSDO);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return userSchedulerJobResultSDOs;
        }

        /// <summary>
        /// Chuyển dữ liệu thô từ db trả về
        /// </summary>
        /// <param name="userSchedulerJobSDO"></param>
        /// <returns></returns>
        private UserSchedulerJobResultSDO MakeSchedulerJobResult(UserSchedulerJobSDO userSchedulerJobSDO)
        {
            UserSchedulerJobResultSDO userSchedulerJobResultSDO = new UserSchedulerJobResultSDO();
            try
            {
                if (userSchedulerJobSDO != null)
                {
                    userSchedulerJobResultSDO.ENABLED = userSchedulerJobSDO.ENABLED == "TRUE" ? true : false;
                    userSchedulerJobResultSDO.FAILURE_COUNT = userSchedulerJobSDO.FAILURE_COUNT;
                    userSchedulerJobResultSDO.JOB_NAME = userSchedulerJobSDO.JOB_NAME;
                    if (userSchedulerJobSDO.LAST_RUN_DURATION.HasValue)
                    {
                        userSchedulerJobResultSDO.LAST_RUN_DURATION = String.Format("{0} giờ {1} phút {2} giây",
                            userSchedulerJobSDO.LAST_RUN_DURATION.Value.Hours, userSchedulerJobSDO.LAST_RUN_DURATION.Value.Minutes, userSchedulerJobSDO.LAST_RUN_DURATION.Value.Seconds);
                    }
                    if (userSchedulerJobSDO.LAST_START_DATE != null)
                    {
                        userSchedulerJobResultSDO.LAST_START_DATE = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(userSchedulerJobSDO.LAST_START_DATE) ?? 0;
                    }

                    if (userSchedulerJobSDO.NEXT_RUN_DATE != null)
                    {
                        userSchedulerJobResultSDO.NEXT_RUN_DATE = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(userSchedulerJobSDO.NEXT_RUN_DATE) ?? 0;
                    }
                    userSchedulerJobResultSDO.REPEAT_INTERVAL = userSchedulerJobSDO.REPEAT_INTERVAL;
                    userSchedulerJobResultSDO.RUN_COUNT = userSchedulerJobSDO.RUN_COUNT;
                    if (userSchedulerJobSDO.START_DATE != null)
                    {
                        userSchedulerJobResultSDO.START_DATE = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(userSchedulerJobSDO.START_DATE) ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                userSchedulerJobResultSDO = null;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return userSchedulerJobResultSDO;
        }
    }
}
