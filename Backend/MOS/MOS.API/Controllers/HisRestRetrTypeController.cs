using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisRestRetrTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRestRetrTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRestRetrTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_REST_RETR_TYPE>> result = new ApiResultObject<List<HIS_REST_RETR_TYPE>>(null);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisRestRetrTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisRestRetrTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_REST_RETR_TYPE>> result = new ApiResultObject<List<V_HIS_REST_RETR_TYPE>>(null);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_REST_RETR_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_REST_RETR_TYPE> result = new ApiResultObject<HIS_REST_RETR_TYPE>(null);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_REST_RETR_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_REST_RETR_TYPE> result = new ApiResultObject<HIS_REST_RETR_TYPE>(null);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
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
        [ActionName("DeleteByRehaServiceTypeId")]
        public ApiResult DeleteByRehaServiceTypeId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
                    result = mng.DeleteByRehaServiceTypeId(param.ApiData);
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
        [ActionName("DeleteByRehaTrainTypeId")]
        public ApiResult DeleteByRehaTrainTypeId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
                    result = mng.DeleteByRehaTrainTypeId(param.ApiData);
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
                ApiResultObject<HIS_REST_RETR_TYPE> result = new ApiResultObject<HIS_REST_RETR_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_REST_RETR_TYPE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_REST_RETR_TYPE>> result = new ApiResultObject<List<HIS_REST_RETR_TYPE>>(null);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<HIS_REST_RETR_TYPE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_REST_RETR_TYPE>> result = new ApiResultObject<List<HIS_REST_RETR_TYPE>>(null);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRestRetrTypeManager mng = new HisRestRetrTypeManager(param.CommonParam);
                    result = mng.DeleteList(param.ApiData);
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
