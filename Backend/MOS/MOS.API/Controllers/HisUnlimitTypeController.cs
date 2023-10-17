using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisUnlimitType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisUnlimitTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisUnlimitTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisUnlimitTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_UNLIMIT_TYPE>> result = new ApiResultObject<List<HIS_UNLIMIT_TYPE>>(null);
                if (param != null)
                {
                    HisUnlimitTypeManager mng = new HisUnlimitTypeManager(param.CommonParam);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_UNLIMIT_TYPE> result = new ApiResultObject<HIS_UNLIMIT_TYPE>(null);
                if (param != null)
                {
                    HisUnlimitTypeManager mng = new HisUnlimitTypeManager(param.CommonParam);
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
            ApiResultObject<HIS_UNLIMIT_TYPE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisUnlimitTypeManager mng = new HisUnlimitTypeManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
