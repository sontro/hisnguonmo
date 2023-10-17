using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDebateReason;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDebateReasonController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDebateReasonViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisDebateReasonViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DEBATE_REASON>> result = new ApiResultObject<List<V_HIS_DEBATE_REASON>>(null);
                if (param != null)
                {
                    HisDebateReasonManager mng = new HisDebateReasonManager(param.CommonParam);
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
