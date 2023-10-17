using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMrCheckSummary;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMrCheckSummaryController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMrCheckSummaryViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMrCheckSummaryViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MR_CHECK_SUMMARY>> result = new ApiResultObject<List<V_HIS_MR_CHECK_SUMMARY>>(null);
                if (param != null)
                {
                    HisMrCheckSummaryManager mng = new HisMrCheckSummaryManager(param.CommonParam);
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
