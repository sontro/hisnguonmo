using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDebateUser;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisDebateUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDebateUserFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDebateUserFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DEBATE_USER>> result = null;
                if (param != null)
                {
                    HisDebateUserManager mng = new HisDebateUserManager(param.CommonParam);
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
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_DEBATE_USER> param)
        {
            try
            {
                ApiResultObject<HIS_DEBATE_USER> result = null;
                if (param != null)
                {
                    HisDebateUserManager mng = new HisDebateUserManager(param.CommonParam);
                    result = mng.Create(param.ApiData);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_DEBATE_USER> param)
        {
            try
            {
                
                ApiResultObject<HIS_DEBATE_USER> result = null;
                if (param != null)
                {
                    HisDebateUserManager mng = new HisDebateUserManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
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
        public ApiResult ChangeLock(ApiParam<HIS_DEBATE_USER> param)
        {
            ApiResultObject<HIS_DEBATE_USER> result = new ApiResultObject<HIS_DEBATE_USER>(null);
            if (param != null && param.ApiData != null)
            {
                HisDebateUserManager mng = new HisDebateUserManager(param.CommonParam);
                result = mng.ChangeLock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
