using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRehaTrain;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisRehaTrainController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRehaTrainFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRehaTrainFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_REHA_TRAIN>> result = new ApiResultObject<List<HIS_REHA_TRAIN>>(null);
                if (param != null)
                {
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisRehaTrainViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisRehaTrainViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_REHA_TRAIN>> result = new ApiResultObject<List<V_HIS_REHA_TRAIN>>(null);
                if (param != null)
                {
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewByRehaSumId")]
        public ApiResult GetViewByRehaSumId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_REHA_TRAIN>> result = new ApiResultObject<List<V_HIS_REHA_TRAIN>>(null);
                if (param != null)
                {
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
                    result = mng.GetViewByRehaSumId(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewByServiceReqId")]
        public ApiResult GetViewByServiceReqId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_REHA_TRAIN>> result = new ApiResultObject<List<V_HIS_REHA_TRAIN>>(null);
                if (param != null)
                {
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
                    result = mng.GetViewByServiceReqId(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_REHA_TRAIN> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN> result = new ApiResultObject<HIS_REHA_TRAIN>(null);
                if (param != null)
                {
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_REHA_TRAIN>> param)
        {
            try
            {
                ApiResultObject<List<HIS_REHA_TRAIN>> result = new ApiResultObject<List<HIS_REHA_TRAIN>>(null);
                if (param != null)
                {
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_REHA_TRAIN> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN> result = new ApiResultObject<HIS_REHA_TRAIN>(null);
                if (param != null)
                {
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
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
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_REHA_TRAIN> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN> result = new ApiResultObject<HIS_REHA_TRAIN>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRehaTrainManager mng = new HisRehaTrainManager(param.CommonParam);
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
