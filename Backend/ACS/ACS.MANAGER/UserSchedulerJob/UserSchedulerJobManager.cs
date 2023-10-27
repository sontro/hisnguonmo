using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using ACS.Filter;
using Inventec.Backend.MANAGER;

namespace ACS.MANAGER.UserSchedulerJob
{

    public class UserSchedulerJobManager : ACS.MANAGER.Base.BusinessBase
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
    }
}
