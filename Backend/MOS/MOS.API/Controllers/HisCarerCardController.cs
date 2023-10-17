using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCarerCard;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisCarerCardController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCarerCardFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCarerCardFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARER_CARD>> result = new ApiResultObject<List<HIS_CARER_CARD>>(null);
                if (param != null)
                {
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_CARER_CARD> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD> result = new ApiResultObject<HIS_CARER_CARD>(null);
                if (param != null)
                {
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_CARER_CARD>> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARER_CARD>> result = new ApiResultObject<List<HIS_CARER_CARD>>(null);
                if (param != null)
                {
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_CARER_CARD> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD> result = new ApiResultObject<HIS_CARER_CARD>(null);
                if (param != null)
                {
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
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
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
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
                ApiResultObject<HIS_CARER_CARD> result = new ApiResultObject<HIS_CARER_CARD>(null);
                if (param != null && param.ApiData != null)
                {
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
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
            ApiResultObject<HIS_CARER_CARD> result = null;
            if (param != null && param.ApiData != null)
            {
                HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Lost")]
        public ApiResult Lost(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
                    result = mng.Lost(param.ApiData);
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
        [ActionName("CancelLost")]
        public ApiResult CancelLost(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
                    result = mng.CancelLost(param.ApiData);
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
