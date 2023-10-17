using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRationSchedule;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisRationScheduleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRationScheduleFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRationScheduleFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_RATION_SCHEDULE>> result = new ApiResultObject<List<HIS_RATION_SCHEDULE>>(null);
                if (param != null)
                {
                    HisRationScheduleManager mng = new HisRationScheduleManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<RationScheduleSDO> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SCHEDULE> result = new ApiResultObject<HIS_RATION_SCHEDULE>(null);
                if (param != null)
                {
                    HisRationScheduleManager mng = new HisRationScheduleManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<RationScheduleSDO> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SCHEDULE> result = new ApiResultObject<HIS_RATION_SCHEDULE>(null);
                if (param != null)
                {
                    HisRationScheduleManager mng = new HisRationScheduleManager(param.CommonParam);
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
                    HisRationScheduleManager mng = new HisRationScheduleManager(param.CommonParam);
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
                ApiResultObject<HIS_RATION_SCHEDULE> result = new ApiResultObject<HIS_RATION_SCHEDULE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRationScheduleManager mng = new HisRationScheduleManager(param.CommonParam);
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
            ApiResultObject<HIS_RATION_SCHEDULE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisRationScheduleManager mng = new HisRationScheduleManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Execute")]
        public ApiResult Execute(ApiParam<RationScheduleExecuteSDO> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_REQ_10>> result = new ApiResultObject<List<V_HIS_SERVICE_REQ_10>>(null);
                if (param != null)
                {
                    HisRationScheduleManager mng = new HisRationScheduleManager(param.CommonParam);
                    result = mng.Execute(param.ApiData);
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
