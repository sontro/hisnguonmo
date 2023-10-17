using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRehaTrainUnit;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisRehaTrainUnitController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRehaTrainUnitFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRehaTrainUnitFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_REHA_TRAIN_UNIT>> result = new ApiResultObject<List<HIS_REHA_TRAIN_UNIT>>(null);
                if (param != null)
                {
                    HisRehaTrainUnitManager mng = new HisRehaTrainUnitManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_REHA_TRAIN_UNIT> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN_UNIT> result = new ApiResultObject<HIS_REHA_TRAIN_UNIT>(null);
                if (param != null)
                {
                    HisRehaTrainUnitManager mng = new HisRehaTrainUnitManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_REHA_TRAIN_UNIT> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN_UNIT> result = new ApiResultObject<HIS_REHA_TRAIN_UNIT>(null);
                if (param != null)
                {
                    HisRehaTrainUnitManager mng = new HisRehaTrainUnitManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_REHA_TRAIN_UNIT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRehaTrainUnitManager mng = new HisRehaTrainUnitManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_REHA_TRAIN_UNIT> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_TRAIN_UNIT> result = new ApiResultObject<HIS_REHA_TRAIN_UNIT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRehaTrainUnitManager mng = new HisRehaTrainUnitManager(param.CommonParam);
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
