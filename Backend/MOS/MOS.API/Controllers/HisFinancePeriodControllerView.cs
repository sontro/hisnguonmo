using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisFinancePeriod;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisFinancePeriodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisFinancePeriodViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisFinancePeriodViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_FINANCE_PERIOD>> result = new ApiResultObject<List<V_HIS_FINANCE_PERIOD>>(null);
                if (param != null)
                {
                    HisFinancePeriodManager mng = new HisFinancePeriodManager(param.CommonParam);
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
