using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDepositReason;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDepositReasonController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDepositReasonViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisDepositReasonViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DEPOSIT_REASON>> result = new ApiResultObject<List<V_HIS_DEPOSIT_REASON>>(null);
                if (param != null)
                {
                    HisDepositReasonManager mng = new HisDepositReasonManager(param.CommonParam);
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
