using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAccidentHurtType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisAccidentHurtTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAccidentHurtTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAccidentHurtTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACCIDENT_HURT_TYPE>> result = new ApiResultObject<List<HIS_ACCIDENT_HURT_TYPE>>(null);
                if (param != null)
                {
                    HisAccidentHurtTypeManager mng = new HisAccidentHurtTypeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ACCIDENT_HURT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_HURT_TYPE> result = new ApiResultObject<HIS_ACCIDENT_HURT_TYPE>(null);
                if (param != null)
                {
                    HisAccidentHurtTypeManager mng = new HisAccidentHurtTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ACCIDENT_HURT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_HURT_TYPE> result = new ApiResultObject<HIS_ACCIDENT_HURT_TYPE>(null);
                if (param != null)
                {
                    HisAccidentHurtTypeManager mng = new HisAccidentHurtTypeManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ACCIDENT_HURT_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAccidentHurtTypeManager mng = new HisAccidentHurtTypeManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_ACCIDENT_HURT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_HURT_TYPE> result = new ApiResultObject<HIS_ACCIDENT_HURT_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAccidentHurtTypeManager mng = new HisAccidentHurtTypeManager(param.CommonParam);
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
