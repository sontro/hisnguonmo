using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHoldReturn;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisHoldReturnController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHoldReturnViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisHoldReturnViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_HOLD_RETURN>> result = new ApiResultObject<List<V_HIS_HOLD_RETURN>>(null);
                if (param != null)
                {
                    HisHoldReturnManager mng = new HisHoldReturnManager(param.CommonParam);
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
    }
}
