using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHoldReturn;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisHoldReturnController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHoldReturnFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisHoldReturnFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_HOLD_RETURN>> result = new ApiResultObject<List<HIS_HOLD_RETURN>>(null);
                if (param != null)
                {
                    HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisHoldReturnCreateSDO> param)
        {
            try
            {
                ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
                if (param != null)
                {
                    HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisHoldReturnUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
                if (param != null)
                {
                    HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
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
                    HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
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
                ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
                if (param != null && param.ApiData != null)
                {
                    HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
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
            ApiResultObject<HIS_HOLD_RETURN> result = null;
            if (param != null && param.ApiData != null)
            {
                HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }


        [HttpPost]
        [ActionName("Return")]
        public ApiResult Return(ApiParam<HisHoldReturnSDO> param)
        {
            try
            {
                ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
                if (param != null)
                {
                    HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
                    result = mng.Return(param.ApiData);
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
        [ActionName("CancelReturn")]
        public ApiResult CancelReturn(ApiParam<HisHoldReturnSDO> param)
        {
            try
            {
                ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
                if (param != null)
                {
                    HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
                    result = mng.CancelReturn(param.ApiData);
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
