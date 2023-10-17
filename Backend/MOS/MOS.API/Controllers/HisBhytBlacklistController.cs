using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBhytBlacklist;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBhytBlacklistController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBhytBlacklistFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBhytBlacklistFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BHYT_BLACKLIST>> result = new ApiResultObject<List<HIS_BHYT_BLACKLIST>>(null);
                if (param != null)
                {
                    HisBhytBlacklistManager mng = new HisBhytBlacklistManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_BHYT_BLACKLIST> param)
        {
            try
            {
                ApiResultObject<HIS_BHYT_BLACKLIST> result = new ApiResultObject<HIS_BHYT_BLACKLIST>(null);
                if (param != null)
                {
                    HisBhytBlacklistManager mng = new HisBhytBlacklistManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_BHYT_BLACKLIST> param)
        {
            try
            {
                ApiResultObject<HIS_BHYT_BLACKLIST> result = new ApiResultObject<HIS_BHYT_BLACKLIST>(null);
                if (param != null)
                {
                    HisBhytBlacklistManager mng = new HisBhytBlacklistManager(param.CommonParam);
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
                    HisBhytBlacklistManager mng = new HisBhytBlacklistManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_BHYT_BLACKLIST> result = new ApiResultObject<HIS_BHYT_BLACKLIST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBhytBlacklistManager mng = new HisBhytBlacklistManager(param.CommonParam);
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
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_BHYT_BLACKLIST> result = null;
            if (param != null && param.ApiData != null)
            {
                HisBhytBlacklistManager mng = new HisBhytBlacklistManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
