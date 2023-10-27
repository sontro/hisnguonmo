using Inventec.Common.Logging;
using Inventec.Core;
using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsAuthenRequest;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsAuthenRequestController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<AcsAuthenRequestFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<AcsAuthenRequestFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_AUTHEN_REQUEST>> result = new ApiResultObject<List<ACS_AUTHEN_REQUEST>>(null);
                if (param != null)
                {
                    AcsAuthenRequestManager mng = new AcsAuthenRequestManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<ACS_AUTHEN_REQUEST> param)
        {
            try
            {
                ApiResultObject<ACS_AUTHEN_REQUEST> result = new ApiResultObject<ACS_AUTHEN_REQUEST>(null);
                if (param != null)
                {
                    AcsAuthenRequestManager mng = new AcsAuthenRequestManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<ACS_AUTHEN_REQUEST> param)
        {
            try
            {
                ApiResultObject<ACS_AUTHEN_REQUEST> result = new ApiResultObject<ACS_AUTHEN_REQUEST>(null);
                if (param != null)
                {
                    AcsAuthenRequestManager mng = new AcsAuthenRequestManager(param.CommonParam);
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
                    AcsAuthenRequestManager mng = new AcsAuthenRequestManager(param.CommonParam);
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
                ApiResultObject<ACS_AUTHEN_REQUEST> result = new ApiResultObject<ACS_AUTHEN_REQUEST>(null);
                if (param != null)
                {
                    AcsAuthenRequestManager mng = new AcsAuthenRequestManager(param.CommonParam);
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
            ApiResultObject<ACS_AUTHEN_REQUEST> result = null;
            if (param != null)
            {
                AcsAuthenRequestManager mng = new AcsAuthenRequestManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
