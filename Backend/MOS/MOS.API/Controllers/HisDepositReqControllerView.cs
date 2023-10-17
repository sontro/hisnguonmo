using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDepositReq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDepositReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDepositReqViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisDepositReqViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DEPOSIT_REQ>> result = new ApiResultObject<List<V_HIS_DEPOSIT_REQ>>(null);
                if (param != null)
                {
                    HisDepositReqManager mng = new HisDepositReqManager(param.CommonParam);
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
