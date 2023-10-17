using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBloodAbo;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBloodAboController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBloodAboFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBloodAboFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BLOOD_ABO>> result = new ApiResultObject<List<HIS_BLOOD_ABO>>(null);
                if (param != null)
                {
                    HisBloodAboManager mng = new HisBloodAboManager(param.CommonParam);
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
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_BLOOD_ABO> result = null;
            if (param != null)
            {
                HisBloodAboManager mng = new HisBloodAboManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
