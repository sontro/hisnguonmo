using Inventec.Common.Logging;
using Inventec.Core;
using ACS.API.Base;
using ACS.Filter;
using ACS.MANAGER.UserSchedulerJob;
using ACS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public class UserSchedulerJobController : ApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<UserSchedulerJobFilter>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<UserSchedulerJobFilter> param)
        {
            try
            {
                ApiResultObject<List<UserSchedulerJobResultSDO>> result = null;
                if (param != null)
                {
                    UserSchedulerJobManager mng = new UserSchedulerJobManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
