using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.Filter;
using MOS.MANAGER.UserSchedulerJob;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
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

        [HttpPost]
        [ActionName("Run")]
        public ApiResult Run(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    UserSchedulerJobManager mng = new UserSchedulerJobManager(param.CommonParam);
                    result = mng.Run(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    UserSchedulerJobManager mng = new UserSchedulerJobManager(param.CommonParam);
                    result = mng.ChangeLock(param.ApiData);
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
