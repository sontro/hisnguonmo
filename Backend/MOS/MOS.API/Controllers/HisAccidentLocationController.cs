using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAccidentLocation;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisAccidentLocationController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAccidentLocationFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAccidentLocationFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACCIDENT_LOCATION>> result = new ApiResultObject<List<HIS_ACCIDENT_LOCATION>>(null);
                if (param != null)
                {
                    HisAccidentLocationManager mng = new HisAccidentLocationManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ACCIDENT_LOCATION> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_LOCATION> result = new ApiResultObject<HIS_ACCIDENT_LOCATION>(null);
                if (param != null)
                {
                    HisAccidentLocationManager mng = new HisAccidentLocationManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ACCIDENT_LOCATION> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_LOCATION> result = new ApiResultObject<HIS_ACCIDENT_LOCATION>(null);
                if (param != null)
                {
                    HisAccidentLocationManager mng = new HisAccidentLocationManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ACCIDENT_LOCATION> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAccidentLocationManager mng = new HisAccidentLocationManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_ACCIDENT_LOCATION> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_LOCATION> result = new ApiResultObject<HIS_ACCIDENT_LOCATION>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAccidentLocationManager mng = new HisAccidentLocationManager(param.CommonParam);
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
