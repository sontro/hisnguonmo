using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDeathWithin;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisDeathWithinController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDeathWithinFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDeathWithinFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DEATH_WITHIN>> result = new ApiResultObject<List<HIS_DEATH_WITHIN>>(null);
                if (param != null)
                {
                    HisDeathWithinManager mng = new HisDeathWithinManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_DEATH_WITHIN> param)
        {
            try
            {
                ApiResultObject<HIS_DEATH_WITHIN> result = new ApiResultObject<HIS_DEATH_WITHIN>(null);
                if (param != null)
                {
                    HisDeathWithinManager mng = new HisDeathWithinManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_DEATH_WITHIN> param)
        {
            try
            {
                ApiResultObject<HIS_DEATH_WITHIN> result = new ApiResultObject<HIS_DEATH_WITHIN>(null);
                if (param != null)
                {
                    HisDeathWithinManager mng = new HisDeathWithinManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_DEATH_WITHIN> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisDeathWithinManager mng = new HisDeathWithinManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_DEATH_WITHIN> param)
        {
            try
            {
                ApiResultObject<HIS_DEATH_WITHIN> result = new ApiResultObject<HIS_DEATH_WITHIN>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDeathWithinManager mng = new HisDeathWithinManager(param.CommonParam);
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
