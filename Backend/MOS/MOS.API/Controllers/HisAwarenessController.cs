using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAwareness;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisAwarenessController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAwarenessFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAwarenessFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_AWARENESS>> result = new ApiResultObject<List<HIS_AWARENESS>>(null);
                if (param != null)
                {
                    HisAwarenessManager mng = new HisAwarenessManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_AWARENESS> param)
        {
            try
            {
                ApiResultObject<HIS_AWARENESS> result = new ApiResultObject<HIS_AWARENESS>(null);
                if (param != null)
                {
                    HisAwarenessManager mng = new HisAwarenessManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_AWARENESS> param)
        {
            try
            {
                ApiResultObject<HIS_AWARENESS> result = new ApiResultObject<HIS_AWARENESS>(null);
                if (param != null)
                {
                    HisAwarenessManager mng = new HisAwarenessManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_AWARENESS> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAwarenessManager mng = new HisAwarenessManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_AWARENESS> param)
        {
            try
            {
                ApiResultObject<HIS_AWARENESS> result = new ApiResultObject<HIS_AWARENESS>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAwarenessManager mng = new HisAwarenessManager(param.CommonParam);
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
