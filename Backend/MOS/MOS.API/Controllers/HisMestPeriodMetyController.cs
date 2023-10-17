using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPeriodMety;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMestPeriodMetyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodMetyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestPeriodMetyFilterQuery> param)
        {
            try
            {
				ApiResultObject<List<HIS_MEST_PERIOD_METY>> result = new ApiResultObject<List<HIS_MEST_PERIOD_METY>>(null);
				if (param != null)
				{
					HisMestPeriodMetyManager mng = new HisMestPeriodMetyManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodMetyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMestPeriodMetyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_PERIOD_METY>> result = new ApiResultObject<List<V_HIS_MEST_PERIOD_METY>>(null);
				if (param != null)
				{
					HisMestPeriodMetyManager mng = new HisMestPeriodMetyManager(param.CommonParam);
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
