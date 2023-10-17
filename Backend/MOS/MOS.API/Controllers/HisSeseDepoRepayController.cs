using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSeseDepoRepay;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisSeseDepoRepayController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSeseDepoRepayFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSeseDepoRepayFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SESE_DEPO_REPAY>> result = new ApiResultObject<List<HIS_SESE_DEPO_REPAY>>(null);
                if (param != null)
                {
                    HisSeseDepoRepayManager mng = new HisSeseDepoRepayManager(param.CommonParam);
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
    }
}
