using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRationSumStt;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisRationSumSttController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRationSumSttFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRationSumSttFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_RATION_SUM_STT>> result = new ApiResultObject<List<HIS_RATION_SUM_STT>>(null);
                if (param != null)
                {
                    HisRationSumSttManager mng = new HisRationSumSttManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_RATION_SUM_STT> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SUM_STT> result = new ApiResultObject<HIS_RATION_SUM_STT>(null);
                if (param != null)
                {
                    HisRationSumSttManager mng = new HisRationSumSttManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_RATION_SUM_STT> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SUM_STT> result = new ApiResultObject<HIS_RATION_SUM_STT>(null);
                if (param != null)
                {
                    HisRationSumSttManager mng = new HisRationSumSttManager(param.CommonParam);
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
                    HisRationSumSttManager mng = new HisRationSumSttManager(param.CommonParam);
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
                ApiResultObject<HIS_RATION_SUM_STT> result = new ApiResultObject<HIS_RATION_SUM_STT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRationSumSttManager mng = new HisRationSumSttManager(param.CommonParam);
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
            ApiResultObject<HIS_RATION_SUM_STT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisRationSumSttManager mng = new HisRationSumSttManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
