using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.AcsUser;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisUserRoom;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.Filter;

namespace MOS.MANAGER.UserSchedulerJob
{

    public class UserSchedulerJobManager : BusinessBase
    {
        public UserSchedulerJobManager()
            : base()
        {

        }

        public UserSchedulerJobManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<UserSchedulerJobResultSDO>> Get(UserSchedulerJobFilter filter)
        {
            ApiResultObject<List<UserSchedulerJobResultSDO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<UserSchedulerJobResultSDO> resultData = null;
                if (valid)
                {
                    resultData = new UserSchedulerJobGetSql(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Run(string data)
        {
            ApiResultObject<bool> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new UserSchedulerJobRun(param).Run(data);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> ChangeLock(string data)
        {
            ApiResultObject<bool> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new UserSchedulerJobLock(param).ChangeLock(data);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
