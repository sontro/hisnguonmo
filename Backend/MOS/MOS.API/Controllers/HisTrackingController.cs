using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTracking;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;
using MOS.TDO;
using MOS.Filter;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisTrackingController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTrackingFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTrackingFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TRACKING>> result = new ApiResultObject<List<HIS_TRACKING>>(null);
                if (param != null)
                {
                    HisTrackingManager mng = new HisTrackingManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisTrackingViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisTrackingViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TRACKING>> result = new ApiResultObject<List<V_HIS_TRACKING>>(null);
                if (param != null)
                {
                    HisTrackingManager mng = new HisTrackingManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
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
        public ApiResult Create(ApiParam<HisTrackingSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRACKING> result = new ApiResultObject<HIS_TRACKING>(null);
                if (param != null)
                {
                    HisTrackingManager mng = new HisTrackingManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisTrackingSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRACKING> result = new ApiResultObject<HIS_TRACKING>(null);
                if (param != null)
                {
                    HisTrackingManager mng = new HisTrackingManager(param.CommonParam);
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
                    HisTrackingManager mng = new HisTrackingManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_TRACKING> param)
        {
            try
            {
                ApiResultObject<HIS_TRACKING> result = new ApiResultObject<HIS_TRACKING>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTrackingManager mng = new HisTrackingManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTrackingForEmrFilter>), "param")]
        [ActionName("GetForEmr")]
        public ApiResult GetForEmr(ApiParam<HisTrackingForEmrFilter> param)
        {
            try
            {
                ApiResultObject<List<HisTrackingTDO>> result = new ApiResultObject<List<HisTrackingTDO>>(null);
                if (param != null)
                {
                    HisTrackingManager mng = new HisTrackingManager(param.CommonParam);
                    result = mng.GetForEmr(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<TrackingDataInputSDO>), "param")]
        [ActionName("GetData")]
        public ApiResult GetData(ApiParam<TrackingDataInputSDO> param)
        {
            try
            {
                ApiResultObject<HisTrackingDataSDO> result = new ApiResultObject<HisTrackingDataSDO>(null);
                if (param != null)
                {
                    HisTrackingManager mng = new HisTrackingManager(param.CommonParam);
                    result = mng.GetData(param.ApiData);
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
