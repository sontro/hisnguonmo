using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisServicePaty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisServicePatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServicePatyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServicePatyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_PATY>> result = new ApiResultObject<List<HIS_SERVICE_PATY>>(null);
                if (param != null)
                {
                    HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServicePatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServicePatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_PATY>> result = new ApiResultObject<List<V_HIS_SERVICE_PATY>>(null);
                if (param != null)
                {
                    HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
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

        [HttpGet]
        [ActionName("GetAppliedView")]
        public ApiResult GetAppliedView(long serviceId, long? treatmentTime)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_PATY>> result = new ApiResultObject<List<V_HIS_SERVICE_PATY>>(null);
                HisServicePatyManager mng = new HisServicePatyManager(new CommonParam());
                result = mng.GetAppliedView(serviceId, treatmentTime);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetApplied")]
        public ApiResult GetApplied(HisServicePatyAppliedFilter filter)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_PATY>> result = new ApiResultObject<List<V_HIS_SERVICE_PATY>>(null);
                HisServicePatyManager mng = new HisServicePatyManager(new CommonParam());
                result = mng.GetAppliedView(filter.SERVICE_ID, filter.TREATMENT_TIME);
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
        public ApiResult Create(ApiParam<HIS_SERVICE_PATY> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_PATY> result = new ApiResultObject<HIS_SERVICE_PATY>(null);
                if (param != null)
                {
                    HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_SERVICE_PATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_PATY>> result = new ApiResultObject<List<HIS_SERVICE_PATY>>(null);
                if (param != null)
                {
                    HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERVICE_PATY> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_PATY> result = new ApiResultObject<HIS_SERVICE_PATY>(null);
                if (param != null)
                {
                    HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<HIS_SERVICE_PATY>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_PATY>> result = new ApiResultObject<List<HIS_SERVICE_PATY>>(null);
                if (param != null)
                {
                    HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
                    result = mng.UpdateList(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_SERVICE_PATY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_SERVICE_PATY> param)
        {
            ApiResultObject<HIS_SERVICE_PATY> result = new ApiResultObject<HIS_SERVICE_PATY>(null);
            if (param != null && param.ApiData != null)
            {
                HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
                result = mng.ChangeLock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServicePatyViewFilterQuery>), "param")]
        [ActionName("GetViewZip")]
        public ApiResultZip GetViewZip(ApiParam<HisServicePatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_PATY>> result = new ApiResultObject<List<V_HIS_SERVICE_PATY>>(null);
                if (param != null)
                {
                    HisServicePatyManager mng = new HisServicePatyManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
                }
                return new ApiResultZip(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
