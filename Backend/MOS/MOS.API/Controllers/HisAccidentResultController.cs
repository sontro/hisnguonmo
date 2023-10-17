using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAccidentResult;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisAccidentResultController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAccidentResultFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAccidentResultFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACCIDENT_RESULT>> result = new ApiResultObject<List<HIS_ACCIDENT_RESULT>>(null);
                if (param != null)
                {
                    HisAccidentResultManager mng = new HisAccidentResultManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ACCIDENT_RESULT> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_RESULT> result = new ApiResultObject<HIS_ACCIDENT_RESULT>(null);
                if (param != null)
                {
                    HisAccidentResultManager mng = new HisAccidentResultManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ACCIDENT_RESULT> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_RESULT> result = new ApiResultObject<HIS_ACCIDENT_RESULT>(null);
                if (param != null)
                {
                    HisAccidentResultManager mng = new HisAccidentResultManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ACCIDENT_RESULT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAccidentResultManager mng = new HisAccidentResultManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_ACCIDENT_RESULT> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_RESULT> result = new ApiResultObject<HIS_ACCIDENT_RESULT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAccidentResultManager mng = new HisAccidentResultManager(param.CommonParam);
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
