using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRehaTrainType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisRehaTrainTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRehaTrainTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRehaTrainTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_REHA_TRAIN_TYPE>> result = new ApiResultObject<List<HIS_REHA_TRAIN_TYPE>>(null);
                if (param != null)
                {
                    HisRehaTrainTypeManager mng = new HisRehaTrainTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisRehaTrainTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisRehaTrainTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_REHA_TRAIN_TYPE>> result = new ApiResultObject<List<V_HIS_REHA_TRAIN_TYPE>>(null);
                if (param != null)
                {
                    HisRehaTrainTypeManager mng = new HisRehaTrainTypeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_REHA_TRAIN_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN_TYPE> result = new ApiResultObject<HIS_REHA_TRAIN_TYPE>(null);
                if (param != null)
                {
                    HisRehaTrainTypeManager mng = new HisRehaTrainTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_REHA_TRAIN_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN_TYPE> result = new ApiResultObject<HIS_REHA_TRAIN_TYPE>(null);
                if (param != null)
                {
                    HisRehaTrainTypeManager mng = new HisRehaTrainTypeManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_REHA_TRAIN_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRehaTrainTypeManager mng = new HisRehaTrainTypeManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_REHA_TRAIN_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN_TYPE> result = new ApiResultObject<HIS_REHA_TRAIN_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRehaTrainTypeManager mng = new HisRehaTrainTypeManager(param.CommonParam);
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
