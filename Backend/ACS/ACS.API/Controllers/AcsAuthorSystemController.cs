using Inventec.Common.Logging;
using Inventec.Core;
using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsAuthorSystem;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsAuthorSystemController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<AcsAuthorSystemFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<AcsAuthorSystemFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_AUTHOR_SYSTEM>> result = new ApiResultObject<List<ACS_AUTHOR_SYSTEM>>(null);
                if (param != null)
                {
                    AcsAuthorSystemManager mng = new AcsAuthorSystemManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<ACS_AUTHOR_SYSTEM> param)
        {
            try
            {
                ApiResultObject<ACS_AUTHOR_SYSTEM> result = new ApiResultObject<ACS_AUTHOR_SYSTEM>(null);
                if (param != null)
                {
                    AcsAuthorSystemManager mng = new AcsAuthorSystemManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<ACS_AUTHOR_SYSTEM> param)
        {
            try
            {
                ApiResultObject<ACS_AUTHOR_SYSTEM> result = new ApiResultObject<ACS_AUTHOR_SYSTEM>(null);
                if (param != null)
                {
                    AcsAuthorSystemManager mng = new AcsAuthorSystemManager(param.CommonParam);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    AcsAuthorSystemManager mng = new AcsAuthorSystemManager(param.CommonParam);
                    result = mng.Delete(param.ApiData);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<ACS_AUTHOR_SYSTEM> result = new ApiResultObject<ACS_AUTHOR_SYSTEM>(null);
                if (param != null)
                {
                    AcsAuthorSystemManager mng = new AcsAuthorSystemManager(param.CommonParam);
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
		
		[HttpPost]
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<ACS_AUTHOR_SYSTEM> result = null;
            if (param != null)
            {
                AcsAuthorSystemManager mng = new AcsAuthorSystemManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
