using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisNumOrderBlock;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisNumOrderBlockController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisNumOrderBlockFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisNumOrderBlockFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_NUM_ORDER_BLOCK>> result = new ApiResultObject<List<HIS_NUM_ORDER_BLOCK>>(null);
                if (param != null)
                {
                    HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisNumOrderBlockOccupiedStatusFilter>), "param")]
        [ActionName("GetOccupiedStatus")]
        public ApiResult GetOccupiedStatus(ApiParam<HisNumOrderBlockOccupiedStatusFilter> param)
        {
            try
            {
                ApiResultObject<List<HisNumOrderBlockSDO>> result = new ApiResultObject<List<HisNumOrderBlockSDO>>(null);
                if (param != null)
                {
                    HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
                    result = mng.GetOccupiedStatus(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_NUM_ORDER_BLOCK> param)
        {
            try
            {
                ApiResultObject<HIS_NUM_ORDER_BLOCK> result = new ApiResultObject<HIS_NUM_ORDER_BLOCK>(null);
                if (param != null)
                {
                    HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_NUM_ORDER_BLOCK>> param)
        {
            try
            {
                ApiResultObject<List<HIS_NUM_ORDER_BLOCK>> result = new ApiResultObject<List<HIS_NUM_ORDER_BLOCK>>(null);
                if (param != null)
                {
                    HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_NUM_ORDER_BLOCK> param)
        {
            try
            {
                ApiResultObject<HIS_NUM_ORDER_BLOCK> result = new ApiResultObject<HIS_NUM_ORDER_BLOCK>(null);
                if (param != null)
                {
                    HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
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
                    HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
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
                ApiResultObject<HIS_NUM_ORDER_BLOCK> result = new ApiResultObject<HIS_NUM_ORDER_BLOCK>(null);
                if (param != null && param.ApiData != null)
                {
                    HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
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
            ApiResultObject<HIS_NUM_ORDER_BLOCK> result = null;
            if (param != null && param.ApiData != null)
            {
                HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
