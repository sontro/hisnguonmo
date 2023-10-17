using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEventsCausesDeath;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisEventsCausesDeathController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEventsCausesDeathFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisEventsCausesDeathFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EVENTS_CAUSES_DEATH>> result = new ApiResultObject<List<HIS_EVENTS_CAUSES_DEATH>>(null);
                if (param != null)
                {
                    HisEventsCausesDeathManager mng = new HisEventsCausesDeathManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_EVENTS_CAUSES_DEATH> param)
        {
            try
            {
                ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = new ApiResultObject<HIS_EVENTS_CAUSES_DEATH>(null);
                if (param != null)
                {
                    HisEventsCausesDeathManager mng = new HisEventsCausesDeathManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_EVENTS_CAUSES_DEATH> param)
        {
            try
            {
                ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = new ApiResultObject<HIS_EVENTS_CAUSES_DEATH>(null);
                if (param != null)
                {
                    HisEventsCausesDeathManager mng = new HisEventsCausesDeathManager(param.CommonParam);
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
                    HisEventsCausesDeathManager mng = new HisEventsCausesDeathManager(param.CommonParam);
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
                ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = new ApiResultObject<HIS_EVENTS_CAUSES_DEATH>(null);
                if (param != null && param.ApiData != null)
                {
                    HisEventsCausesDeathManager mng = new HisEventsCausesDeathManager(param.CommonParam);
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
            ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = null;
            if (param != null && param.ApiData != null)
            {
                HisEventsCausesDeathManager mng = new HisEventsCausesDeathManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
