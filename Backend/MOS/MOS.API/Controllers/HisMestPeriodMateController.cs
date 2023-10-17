using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPeriodMate;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMestPeriodMateController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodMateFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestPeriodMateFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PERIOD_MATE>> result = new ApiResultObject<List<HIS_MEST_PERIOD_MATE>>(null);
                if (param != null)
                {
                    HisMestPeriodMateManager mng = new HisMestPeriodMateManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodMateViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMestPeriodMateViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_PERIOD_MATE>> result = new ApiResultObject<List<V_HIS_MEST_PERIOD_MATE>>(null);
                if (param != null)
                {
                    HisMestPeriodMateManager mng = new HisMestPeriodMateManager(param.CommonParam);
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
