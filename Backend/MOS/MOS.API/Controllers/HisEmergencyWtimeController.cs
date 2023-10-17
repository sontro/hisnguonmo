using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEmergencyWtime;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisEmergencyWtimeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEmergencyWtimeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisEmergencyWtimeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EMERGENCY_WTIME>> result = new ApiResultObject<List<HIS_EMERGENCY_WTIME>>(null);
                if (param != null)
                {
                    HisEmergencyWtimeManager mng = new HisEmergencyWtimeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_EMERGENCY_WTIME> param)
        {
            try
            {
                ApiResultObject<HIS_EMERGENCY_WTIME> result = new ApiResultObject<HIS_EMERGENCY_WTIME>(null);
                if (param != null)
                {
                    HisEmergencyWtimeManager mng = new HisEmergencyWtimeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_EMERGENCY_WTIME> param)
        {
            try
            {
                ApiResultObject<HIS_EMERGENCY_WTIME> result = new ApiResultObject<HIS_EMERGENCY_WTIME>(null);
                if (param != null)
                {
                    HisEmergencyWtimeManager mng = new HisEmergencyWtimeManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_EMERGENCY_WTIME> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisEmergencyWtimeManager mng = new HisEmergencyWtimeManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_EMERGENCY_WTIME> param)
        {
            try
            {
                ApiResultObject<HIS_EMERGENCY_WTIME> result = new ApiResultObject<HIS_EMERGENCY_WTIME>(null);
                if (param != null && param.ApiData != null)
                {
                    HisEmergencyWtimeManager mng = new HisEmergencyWtimeManager(param.CommonParam);
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
